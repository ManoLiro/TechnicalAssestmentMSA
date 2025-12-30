using MediatR;
using TechnicalAssestmentMSA.Application.Clientes.DTOs;

namespace TechnicalAssestmentMSA.Application.Clientes.Queries
{
    public sealed record ObtemClientePorIdQuery(Guid Id) : IRequest<ClienteDto?>;
}
