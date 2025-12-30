namespace TechnicalAssestmentMSA.Domain.Exceptions;

/// <summary>
/// Exceção lançada quando um CNPJ inválido é fornecido
/// </summary>
public sealed class CnpjInvalidoException : DomainException
{
    public string CnpjFornecido { get; }

    public CnpjInvalidoException(string cnpj)
        : base($"O CNPJ '{cnpj}' é inválido.")
    {
        CnpjFornecido = cnpj;
    }

    public CnpjInvalidoException(string cnpj, Exception innerException)
        : base($"O CNPJ '{cnpj}' é inválido.", innerException)
    {
        CnpjFornecido = cnpj;
    }
}