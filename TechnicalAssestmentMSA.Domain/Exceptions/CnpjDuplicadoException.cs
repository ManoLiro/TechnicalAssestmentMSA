namespace TechnicalAssestmentMSA.Domain.Exceptions;

/// <summary>
/// Exceção lançada quando tenta-se cadastrar um CNPJ já existente
/// </summary>
public sealed class CnpjDuplicadoException : DomainException
{
    public string Cnpj { get; }

    public CnpjDuplicadoException(string cnpj)
        : base($"Já existe um cliente cadastrado com o CNPJ '{cnpj}'.")
    {
        Cnpj = cnpj;
    }
}