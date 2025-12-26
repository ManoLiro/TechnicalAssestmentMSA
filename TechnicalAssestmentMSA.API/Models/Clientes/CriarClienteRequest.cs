namespace TechnicalAssestmentMSA.API.Models.Clientes
{
    public sealed record CriarClienteRequest(string NomeFantasia, string Cnpj, bool Ativo);
}
