using MediatR;
using TechnicalAssestmentMSA.Domain.Entidades;

namespace TechnicalAssestmentMSA.Application.Clientes.Queries
{
    public sealed record ObtemClientePorIdQuery(Guid Id) : IRequest<Cliente?>;
}
