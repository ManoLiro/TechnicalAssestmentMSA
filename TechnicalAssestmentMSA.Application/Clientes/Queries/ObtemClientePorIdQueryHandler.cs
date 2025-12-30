using MediatR;
using TechnicalAssestmentMSA.Application.Repositories;
using TechnicalAssestmentMSA.Domain.Entidades;
using Microsoft.Extensions.Logging;
using TechnicalAssestmentMSA.Application.Clientes.DTOs;

namespace TechnicalAssestmentMSA.Application.Clientes.Queries
{
    public sealed class ObtemClientePorIdQueryHandler : IRequestHandler<ObtemClientePorIdQuery, ClienteDto?>
    {
        private readonly ILogger<ObtemClientePorIdQueryHandler> _logger;
        private readonly IClienteRepository _repositorio;

        public ObtemClientePorIdQueryHandler(IClienteRepository repositorio, ILogger<ObtemClientePorIdQueryHandler> logger)
        {
            _repositorio = repositorio;
            _logger = logger;
        }

        public async Task<ClienteDto?> Handle(ObtemClientePorIdQuery query, CancellationToken ct)
        {
            _logger.LogInformation("Buscando cliente por ID: {ClienteId}", query.Id);

            var cliente = await _repositorio.ObterPorIdAsync(query.Id, ct);

            if (cliente is null)
            {
                _logger.LogWarning("Cliente não encontrado: {ClienteId}", query.Id);
                return null;
            }

            _logger.LogInformation("Cliente encontrado: {ClienteId}", query.Id);

            return new ClienteDto(
                cliente.Id,
                cliente.NomeFantasia,
                cliente.Cnpj.Valor,
                cliente.Cnpj.ObterValorFormatado(),
                cliente.Ativo
            );
        }
    }
}
