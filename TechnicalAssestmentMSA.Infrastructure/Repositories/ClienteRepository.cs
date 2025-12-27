using NHibernate;
using NHibernate.Linq;
using TechnicalAssestmentMSA.Application.Repositories;
using TechnicalAssestmentMSA.Domain.Entidades;

namespace TechnicalAssestmentMSA.Infrastructure.Repositories
{
    public sealed class ClienteRepository : IClienteRepository
    {
        private readonly ISession _session;

        public ClienteRepository(ISession session)
            => _session = session;
        public Task AdicionarAsync(Cliente cliente, CancellationToken ct = default)
        {
            return _session.SaveAsync(cliente);
        }

        public async Task<bool> ExisteCnpjAsync(string cnpj, CancellationToken ct = default)
        {
            return await _session.Query<Cliente>()
              .AnyAsync(c => c.Cnpj.Valor == cnpj, ct);
        }

        public Task<Cliente?> ObterPorIdAsync(Guid id, CancellationToken ct = default)
        {
            return _session.GetAsync<Cliente?>(id, ct);
        }
    }
}
