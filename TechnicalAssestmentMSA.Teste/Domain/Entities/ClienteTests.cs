using TechnicalAssestmentMSA.Domain.Entidades;
using TechnicalAssestmentMSA.Domain.ValueObjects;

namespace TechnicalAssestmentMSA.Teste.Domain.Entities
{
    public class ClienteTests
    {
        [Fact]
        public void Construtor_DeveCriarCliente_QuandoDadosSaoValidos()
        {
            // Arrange
            var id = Guid.NewGuid();
            var cnpj = Cnpj.Criar("11222333000181");
            var nomeFantasia = "Empresa Teste Ltda";

            // Act
            var cliente = new Cliente(id, nomeFantasia, cnpj, true);

            // Assert
            Assert.Equal(id, cliente.Id);
            Assert.Equal(nomeFantasia, cliente.NomeFantasia);
            Assert.Equal(cnpj, cliente.Cnpj);
            Assert.True(cliente.Ativo);
        }

        [Fact]
        public void Construtor_DeveLancarExcecao_QuandoIdEhVazio()
        {
            // Arrange
            var cnpj = Cnpj.Criar("11222333000181");

            // Act & Assert
            var excecao = Assert.Throws<Exception>(() => 
                new Cliente(Guid.Empty, "Empresa Teste", cnpj, true));
            
            Assert.Equal("Id do cliente é inválido.", excecao.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void Construtor_DeveLancarExcecao_QuandoNomeFantasiaEhInvalido(string nomeInvalido)
        {
            // Arrange
            var id = Guid.NewGuid();
            var cnpj = Cnpj.Criar("11222333000181");

            // Act & Assert
            var excecao = Assert.Throws<Exception>(() => 
                new Cliente(id, nomeInvalido, cnpj, true));
            
            Assert.Equal("Nome fantasia é obrigatório.", excecao.Message);
        }

        [Fact]
        public void Construtor_DeveLancarExcecao_QuandoCnpjEhNulo()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act & Assert
            var excecao = Assert.Throws<Exception>(() => 
                new Cliente(id, "Empresa Teste", null!, true));
            
            Assert.Equal("CNPJ é obrigatório.", excecao.Message);
        }

        [Fact]
        public void AlterarNomeFantasia_DeveAlterarNome_QuandoNomeEhValido()
        {
            // Arrange
            var id = Guid.NewGuid();
            var cnpj = Cnpj.Criar("11222333000181");
            var cliente = new Cliente(id, "Nome Antigo", cnpj, true);
            var novoNome = "Nome Novo";

            // Act
            cliente.AlterarNomeFantasia(novoNome);

            // Assert
            Assert.Equal(novoNome, cliente.NomeFantasia);
        }

        [Fact]
        public void AlterarNomeFantasia_DeveRemoverEspacos_QuandoNomePossuiEspacosExtras()
        {
            // Arrange
            var id = Guid.NewGuid();
            var cnpj = Cnpj.Criar("11222333000181");
            var cliente = new Cliente(id, "Nome Antigo", cnpj, true);

            // Act
            cliente.AlterarNomeFantasia("  Nome Com Espacos  ");

            // Assert
            Assert.Equal("Nome Com Espacos", cliente.NomeFantasia);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void AlterarNomeFantasia_DeveLancarExcecao_QuandoNomeEhInvalido(string nomeInvalido)
        {
            // Arrange
            var id = Guid.NewGuid();
            var cnpj = Cnpj.Criar("11222333000181");
            var cliente = new Cliente(id, "Nome Válido", cnpj, true);

            // Act & Assert
            var excecao = Assert.Throws<Exception>(() => 
                cliente.AlterarNomeFantasia(nomeInvalido));
            
            Assert.Equal("Nome fantasia é obrigatório.", excecao.Message);
        }

        [Fact]
        public void Ativar_DeveAtivarCliente()
        {
            // Arrange
            var id = Guid.NewGuid();
            var cnpj = Cnpj.Criar("11222333000181");
            var cliente = new Cliente(id, "Empresa Teste", cnpj, false);

            // Act
            cliente.Ativar();

            // Assert
            Assert.True(cliente.Ativo);
        }

        [Fact]
        public void Desativar_DeveDesativarCliente()
        {
            // Arrange
            var id = Guid.NewGuid();
            var cnpj = Cnpj.Criar("11222333000181");
            var cliente = new Cliente(id, "Empresa Teste", cnpj, true);

            // Act
            cliente.Desativar();

            // Assert
            Assert.False(cliente.Ativo);
        }

        [Fact]
        public void Construtor_DeveCriarClienteAtivoComoPadrao_QuandoNaoInformado()
        {
            // Arrange
            var id = Guid.NewGuid();
            var cnpj = Cnpj.Criar("11222333000181");

            // Act
            var cliente = new Cliente(id, "Empresa Teste", cnpj);

            // Assert
            Assert.True(cliente.Ativo);
        }
    }
}
