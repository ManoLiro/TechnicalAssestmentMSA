using MediatR;
using TechnicalAssestmentMSA.Application.Repositories;
using TechnicalAssestmentMSA.Domain.Entidades;
using TechnicalAssestmentMSA.Domain.Exceptions;
using TechnicalAssestmentMSA.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace TechnicalAssestmentMSA.Application.Clientes.Commands;

public sealed class CriaClienteCommandHandler : IRequestHandler<CriaClienteCommand, Guid>
{
    private readonly IClienteRepository _repositorio;
    private readonly IUnitOfWorkRepository _uow;
    private readonly ILogger<CriaClienteCommandHandler> _logger;

    public CriaClienteCommandHandler(
        IClienteRepository repositorio, 
        IUnitOfWorkRepository uow,
        ILogger<CriaClienteCommandHandler> logger)
    {
        _repositorio = repositorio;
        _uow = uow;
        _logger = logger;
    }

    public async Task<Guid> Handle(CriaClienteCommand comando, CancellationToken ct)
    {
        _logger.LogInformation(
            "Iniciando criação de cliente. NomeFantasia: {NomeFantasia}, CNPJ: {Cnpj}", 
            comando.NomeFantasia, 
            comando.Cnpj);

        Cnpj cnpj;
        try
        {
            cnpj = Cnpj.Criar(comando.Cnpj);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "CNPJ inválido fornecido: {Cnpj}", comando.Cnpj);
            throw new CnpjInvalidoException(comando.Cnpj, ex);
        }

        if (await _repositorio.ExisteCnpjAsync(cnpj.Valor, ct))
        {
            _logger.LogWarning("Tentativa de cadastrar CNPJ duplicado: {Cnpj}", cnpj.Valor);
            throw new CnpjDuplicadoException(cnpj.Valor);
        }

        var cliente = new Cliente(
            Guid.NewGuid(),
            comando.NomeFantasia,
            cnpj,
            comando.Ativo);

        await _repositorio.AdicionarAsync(cliente, ct);
        await _uow.CommitAsync(ct);

        _logger.LogInformation(
            "Cliente criado com sucesso. Id: {ClienteId}, CNPJ: {Cnpj}", 
            cliente.Id, 
            cnpj.Valor);

        return cliente.Id;
    }
}