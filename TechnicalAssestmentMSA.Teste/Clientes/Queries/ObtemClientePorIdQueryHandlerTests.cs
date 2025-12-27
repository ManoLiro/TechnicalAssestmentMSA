using Moq;
using TechnicalAssestmentMSA.Application.Clientes.Queries;
using TechnicalAssestmentMSA.Application.Repositories;
using TechnicalAssestmentMSA.Domain.Entidades;
using TechnicalAssestmentMSA.Domain.ValueObjects;

namespace TechnicalAssestmentMSA.Teste.Clientes.Queries
{
    public class ObtemClientePorIdQueryHandlerTests
    {
        private readonly Mock<IClienteRepository> _repositorioMock;
        private readonly ObtemClientePorIdQueryHandler _handler;

        public ObtemClientePorIdQueryHandlerTests()
        {
            _repositorioMock = new Mock<IClienteRepository>();
            _handler = new ObtemClientePorIdQueryHandler(_repositorioMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarCliente_QuandoIdExiste()
        {
            // Arrange
            var clienteId = Guid.NewGuid();
            var cnpj = Cnpj.Criar("11222333000181");
            var clienteEsperado = new Cliente(clienteId, "Empresa Teste Ltda", cnpj, true);
            
            _repositorioMock.Setup(r => r.ObterPorIdAsync(clienteId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(clienteEsperado);

            var query = new ObtemClientePorIdQuery(clienteId);

            // Act
            var resultado = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(clienteId, resultado.Id);
            Assert.Equal("Empresa Teste Ltda", resultado.NomeFantasia);
            Assert.Equal(cnpj.Valor, resultado.Cnpj.Valor);
            Assert.True(resultado.Ativo);
            _repositorioMock.Verify(r => r.ObterPorIdAsync(clienteId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_DeveRetornarNulo_QuandoIdNaoExiste()
        {
            // Arrange
            var clienteId = Guid.NewGuid();
            
            _repositorioMock.Setup(r => r.ObterPorIdAsync(clienteId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Cliente?)null);

            var query = new ObtemClientePorIdQuery(clienteId);

            // Act
            var resultado = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(resultado);
            _repositorioMock.Verify(r => r.ObterPorIdAsync(clienteId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_DeveRetornarClienteCorreto_QuandoExistemMultiplosClientes()
        {
            // Arrange
            var clienteId1 = Guid.NewGuid();
            var clienteId2 = Guid.NewGuid();
            var cnpj = Cnpj.Criar("11222333000181");
            var cliente1 = new Cliente(clienteId1, "Empresa Um", cnpj, true);
            
            _repositorioMock.Setup(r => r.ObterPorIdAsync(clienteId1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(cliente1);
            
            _repositorioMock.Setup(r => r.ObterPorIdAsync(clienteId2, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Cliente?)null);

            var query = new ObtemClientePorIdQuery(clienteId1);

            // Act
            var resultado = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(clienteId1, resultado.Id);
            Assert.Equal("Empresa Um", resultado.NomeFantasia);
        }

        [Fact]
        public async Task Handle_DeveRetornarClienteInativo_QuandoClienteEstiverInativo()
        {
            // Arrange
            var clienteId = Guid.NewGuid();
            var cnpj = Cnpj.Criar("11222333000181");
            var clienteInativo = new Cliente(clienteId, "Empresa Inativa", cnpj, false);
            
            _repositorioMock.Setup(r => r.ObterPorIdAsync(clienteId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(clienteInativo);

            var query = new ObtemClientePorIdQuery(clienteId);

            // Act
            var resultado = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(resultado);
            Assert.False(resultado.Ativo);
        }
    }
}
