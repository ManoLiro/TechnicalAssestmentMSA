namespace TechnicalAssestmentMSA.Application.Clientes.DTOs;

/// <summary>
/// DTO para representação de cliente
/// </summary>
public sealed record ClienteDto(
    Guid Id,
    string NomeFantasia,
    string Cnpj,
    string CnpjFormatado,
    bool Ativo
);