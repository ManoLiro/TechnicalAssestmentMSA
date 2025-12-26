namespace TechnicalAssestmentMSA.Application.Clientes.Commands
{
    public sealed record CriaClienteCommand(string NomeFantasia, string Cnpj, bool Ativo);
}
