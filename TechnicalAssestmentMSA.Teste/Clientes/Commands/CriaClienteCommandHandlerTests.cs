using Moq;
using TechnicalAssestmentMSA.Application.Clientes.Commands;
using TechnicalAssestmentMSA.Application.Repositories;
using TechnicalAssestmentMSA.Domain.Entidades;
using TechnicalAssestmentMSA.Domain.ValueObjects;

namespace TechnicalAssestmentMSA.Teste.Clientes.Commands
{
    public class CriaClienteCommandHandlerTests
    {
        private readonly Mock<IClienteRepository> _repositorioMock;
        private readonly Mock<IUnitOfWorkRepository> _uowMock;
        private readonly CriaClienteCommandHandler _handler;

        public CriaClienteCommandHandlerTests()
        {
            _repositorioMock = new Mock<IClienteRepository>();
            _uowMock = new Mock<IUnitOfWorkRepository>();
            _handler = new CriaClienteCommandHandler(_repositorioMock.Object, _uowMock.Object);
        }

        [Fact]
        public async Task Handle_DeveCriarClienteComSucesso_QuandoDadosSaoValidos()
        {
            // Arrange
            var comando = new CriaClienteCommand("Empresa Teste Ltda", "11222333000181", true);
            _repositorioMock.Setup(r => r.ExisteCnpjAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var resultado = await _handler.Handle(comando, CancellationToken.None);

            // Assert
            Assert.NotEqual(Guid.Empty, resultado);
            _repositorioMock.Verify(r => r.AdicionarAsync(It.IsAny<Cliente>(), It.IsAny<CancellationToken>()), Times.Once);
            _uowMock.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_DeveRetornarErro_QuandoCnpjJaExiste()
        {
            // Arrange
            var comando = new CriaClienteCommand("Empresa Teste Ltda", "11222333000181", true);
            _repositorioMock.Setup(r => r.ExisteCnpjAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act & Assert
            var excecao = await Assert.ThrowsAsync<Exception>(() => 
                _handler.Handle(comando, CancellationToken.None));
            
            Assert.Equal("CNPJ Já Cadastrado", excecao.Message);
            _repositorioMock.Verify(r => r.AdicionarAsync(It.IsAny<Cliente>(), It.IsAny<CancellationToken>()), Times.Never);
            _uowMock.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Handle_DeveRetornarErro_QuandoNomeFantasiaEhVazio()
        {
            // Arrange
            var comando = new CriaClienteCommand("", "11222333000181", true);
            _repositorioMock.Setup(r => r.ExisteCnpjAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act & Assert
            var excecao = await Assert.ThrowsAsync<Exception>(() => 
                _handler.Handle(comando, CancellationToken.None));
            
            Assert.Equal("Nome fantasia é obrigatório.", excecao.Message);
            _repositorioMock.Verify(r => r.AdicionarAsync(It.IsAny<Cliente>(), It.IsAny<CancellationToken>()), Times.Never);
            _uowMock.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Handle_DeveRetornarErro_QuandoNomeFantasiaEhNulo()
        {
            // Arrange
            var comando = new CriaClienteCommand(null!, "11222333000181", true);
            _repositorioMock.Setup(r => r.ExisteCnpjAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act & Assert
            var excecao = await Assert.ThrowsAsync<Exception>(() => 
                _handler.Handle(comando, CancellationToken.None));
            
            Assert.Equal("Nome fantasia é obrigatório.", excecao.Message);
            _repositorioMock.Verify(r => r.AdicionarAsync(It.IsAny<Cliente>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Handle_DeveRetornarErro_QuandoCnpjEhInvalido()
        {
            // Arrange
            var comando = new CriaClienteCommand("Empresa Teste Ltda", "12345678901234", true);
            _repositorioMock.Setup(r => r.ExisteCnpjAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act & Assert
            var excecao = await Assert.ThrowsAsync<Exception>(() => 
                _handler.Handle(comando, CancellationToken.None));
            
            Assert.Contains("CNPJ inválido", excecao.Message);
            _repositorioMock.Verify(r => r.AdicionarAsync(It.IsAny<Cliente>(), It.IsAny<CancellationToken>()), Times.Never);
            _uowMock.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Handle_DeveRetornarErro_QuandoCnpjEhVazio()
        {
            // Arrange
            var comando = new CriaClienteCommand("Empresa Teste Ltda", "", true);
            _repositorioMock.Setup(r => r.ExisteCnpjAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act & Assert
            var excecao = await Assert.ThrowsAsync<Exception>(() => 
                _handler.Handle(comando, CancellationToken.None));
            
            Assert.Contains("CNPJ inválido", excecao.Message);
            _repositorioMock.Verify(r => r.AdicionarAsync(It.IsAny<Cliente>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Handle_DeveCriarClienteAtivo_QuandoFlagAtivoEhTrue()
        {
            // Arrange
            var comando = new CriaClienteCommand("Empresa Teste Ltda", "11222333000181", true);
            Cliente? clienteAdicionado = null;
            
            _repositorioMock.Setup(r => r.ExisteCnpjAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);
            
            _repositorioMock.Setup(r => r.AdicionarAsync(It.IsAny<Cliente>(), It.IsAny<CancellationToken>()))
                .Callback<Cliente, CancellationToken>((c, ct) => clienteAdicionado = c);

            // Act
            await _handler.Handle(comando, CancellationToken.None);

            // Assert
            Assert.NotNull(clienteAdicionado);
            Assert.True(clienteAdicionado.Ativo);
        }

        [Fact]
        public async Task Handle_DeveCriarClienteInativo_QuandoFlagAtivoEhFalse()
        {
            // Arrange
            var comando = new CriaClienteCommand("Empresa Teste Ltda", "11222333000181", false);
            Cliente? clienteAdicionado = null;
            
            _repositorioMock.Setup(r => r.ExisteCnpjAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);
            
            _repositorioMock.Setup(r => r.AdicionarAsync(It.IsAny<Cliente>(), It.IsAny<CancellationToken>()))
                .Callback<Cliente, CancellationToken>((c, ct) => clienteAdicionado = c);

            // Act
            await _handler.Handle(comando, CancellationToken.None);

            // Assert
            Assert.NotNull(clienteAdicionado);
            Assert.False(clienteAdicionado.Ativo);
        }
    }
}
