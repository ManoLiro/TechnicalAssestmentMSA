using NHibernate;
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
            // HQL navegando no componente: c.Cnpj.Valor
            var qtd = await _session.CreateQuery("select count(1) from Cliente c where c.Cnpj.Valor = :cnpj")
                .SetParameter("cnpj", cnpj)
                .UniqueResultAsync<long>(ct);

            return qtd > 0;
        }

        public Task<Cliente?> ObterPorIdAsync(Guid id, CancellationToken ct = default)
        {
           return _session.GetAsync<Cliente?>(id, ct);
        }
    }
}
