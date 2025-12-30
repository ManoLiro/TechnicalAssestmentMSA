using FluentValidation;
using TechnicalAssestmentMSA.API.Models.Clientes;
using TechnicalAssestmentMSA.Domain.ValueObjects;

namespace TechnicalAssestmentMSA.API.Validators;

/// <summary>
/// Validador para requisição de criação de cliente
/// </summary>
public class CriarClienteRequestValidator : AbstractValidator<CriarClienteRequest>
{
    public CriarClienteRequestValidator()
    {
        RuleFor(x => x.NomeFantasia)
            .NotEmpty()
            .WithMessage("Nome fantasia é obrigatório.")
            .MaximumLength(200)
            .WithMessage("Nome fantasia não pode ter mais de 200 caracteres.");

        RuleFor(x => x.Cnpj)
            .NotEmpty()
            .WithMessage("CNPJ é obrigatório.")
            .Must(CnpjValido)
            .WithMessage("CNPJ inválido. Verifique o formato e os dígitos verificadores.");
    }

    private static bool CnpjValido(string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj))
            return false;

        return Cnpj.TentarConverter(cnpj, out _);
    }
}