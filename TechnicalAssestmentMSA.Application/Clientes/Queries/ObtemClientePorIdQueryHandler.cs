using MediatR;
using TechnicalAssestmentMSA.Application.Repositories;
using TechnicalAssestmentMSA.Domain.Entidades;

namespace TechnicalAssestmentMSA.Application.Clientes.Queries
{
    public sealed class ObtemClientePorIdQueryHandler : IRequestHandler<ObtemClientePorIdQuery, Cliente?>
    {
        private readonly IClienteRepository _repositorio;

        public ObtemClientePorIdQueryHandler(IClienteRepository repositorio)
            => _repositorio = repositorio;

        public Task<Cliente?> Handle(ObtemClientePorIdQuery query, CancellationToken ct)
            => _repositorio.ObterPorIdAsync(query.Id, ct);
    }
}
