using TechnicalAssestmentMSA.Domain.Entidades;

namespace TechnicalAssestmentMSA.Application.Repositories
{
    public interface IClienteRepository
    {
        Task AdicionarAsync(Cliente cliente, CancellationToken ct = default);
        Task<Cliente?> ObterPorIdAsync(Guid id, CancellationToken ct = default);
        Task<bool> ExisteCnpjAsync(string cnpj, CancellationToken ct = default);
    }
}
