using TechnicalAssestmentMSA.Application.Repositories;
using TechnicalAssestmentMSA.Domain.Entidades;
using TechnicalAssestmentMSA.Domain.ValueObjects;

namespace TechnicalAssestmentMSA.Application.Clientes.Commands
{
    public sealed class CriaClienteCommandHandler
    {
        private readonly IClienteRepository _repositorio;
        private readonly IUnidadeDeTrabalho _uow;

        public CriaClienteCommandHandler(IClienteRepository repositorio, IUnidadeDeTrabalho uow)
        {
            _repositorio = repositorio;
            _uow = uow;
        }

        public async Task<Guid> HandleAsync(CriaClienteCommand comando, CancellationToken ct = default)
        {
            Cnpj cnpj;
            try
            {
                cnpj = Cnpj.Criar(comando.Cnpj);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            if (await _repositorio.ExisteCnpjAsync(cnpj.Valor, ct))
                throw new Exception("CNPJ Já Cadastrado");

            Cliente cliente;
            try
            {
                cliente = new Cliente(Guid.NewGuid(), comando.NomeFantasia, cnpj, comando.Ativo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            await _repositorio.AdicionarAsync(cliente, ct);
            await _uow.CommitAsync(ct);

            return cliente.Id;
        }
    }
}
