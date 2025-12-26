using TechnicalAssestmentMSA.Application.Repositories;
using TechnicalAssestmentMSA.Domain.Entidades;

namespace TechnicalAssestmentMSA.Application.Clientes.Queries
{
    public sealed class ObtemClientePorIdQueryHandler
    {
        private readonly IClienteRepository _repositorio;

        public ObtemClientePorIdQueryHandler(IClienteRepository repositorio)
            => _repositorio = repositorio;

        public Task<Cliente?> HandleAsync(ObtemClientePorIdQuery query, CancellationToken ct = default)
            => _repositorio.ObterPorIdAsync(query.Id, ct);
    }
}
