using TechnicalAssestmentMSA.Domain.ValueObjects;

namespace TechnicalAssestmentMSA.Teste.Domain.ValueObjects
{
    public class CnpjTests
    {
        [Theory]
        [InlineData("11.222.333/0001-81")]
        [InlineData("11222333000181")]
        public void Criar_DeveCriarCnpjValido_QuandoFormatoEhCorreto(string cnpjEntrada)
        {
            // Act
            var cnpj = Cnpj.Criar(cnpjEntrada);

            // Assert
            Assert.NotNull(cnpj);
            Assert.Equal("11222333000181", cnpj.Valor);
        }

        [Theory]
        [InlineData("12345678901234")]
        [InlineData("11111111111111")]
        public void Criar_DeveLancarExcecao_QuandoCnpjEhInvalido(string cnpjInvalido)
        {
            // Act & Assert
            var excecao = Assert.Throws<ArgumentException>(() => Cnpj.Criar(cnpjInvalido));
            Assert.Contains("CNPJ inválido", excecao.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public void Criar_DeveLancarExcecao_QuandoCnpjEhVazioOuNulo(string cnpjVazio)
        {
            // Act & Assert
            var excecao = Assert.Throws<ArgumentException>(() => Cnpj.Criar(cnpjVazio));
            Assert.Equal("CNPJ inválido. (Parameter 'valor')", excecao.Message);
        }

        [Fact]
        public void TentarConverter_DeveRetornarTrue_QuandoCnpjEhValido()
        {
            // Arrange
            var cnpjValido = "11222333000181";

            // Act
            var resultado = Cnpj.TentarConverter(cnpjValido, out var cnpj);

            // Assert
            Assert.True(resultado);
            Assert.NotNull(cnpj);
            Assert.Equal("11222333000181", cnpj.Valor);
        }

        [Fact]
        public void TentarConverter_DeveRetornarFalse_QuandoCnpjEhInvalido()
        {
            // Arrange
            var cnpjInvalido = "12345678901234";

            // Act
            var resultado = Cnpj.TentarConverter(cnpjInvalido, out var cnpj);

            // Assert
            Assert.False(resultado);
            Assert.Null(cnpj);
        }

        [Fact]
        public void ObterValorFormatado_DeveRetornarCnpjFormatado()
        {
            // Arrange
            var cnpj = Cnpj.Criar("11222333000181");

            // Act
            var formatado = cnpj.ObterValorFormatado();

            // Assert
            Assert.Equal("11.222.333/0001-81", formatado);
        }
    }
}
