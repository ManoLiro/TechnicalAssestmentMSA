using System.Text.RegularExpressions;

namespace TechnicalAssestmentMSA.Domain.ValueObjects
{
    public sealed record Cnpj
    {
        public string Valor { get; init; }

        private static readonly Regex RegexCnpj =
            new(@"^[A-Z0-9]{12}\d{2}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static readonly int[] PesosPrimeiroDigito = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
        private static readonly int[] PesosSegundoDigito = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

        public static Cnpj Criar(string valor)
        {
            if (!TentarConverter(valor, out var cnpj))
                throw new ArgumentException("CNPJ inválido.", nameof(valor));

            return cnpj;
        }

        private Cnpj(string valorSanitizado)
        {
            Valor = valorSanitizado;
        }

        public static bool TentarConverter(string entrada, out Cnpj cnpj)
        {
            cnpj = null;

            if (string.IsNullOrWhiteSpace(entrada))
                return false;

            string sanitizado = Sanitizar(entrada);

            if (!FormatoValido(sanitizado))
                return false;

            string numeroBase = sanitizado[..12];
            string digitosInformados = sanitizado[12..14];

            int digito1Calculado = CalcularDigitoVerificador(numeroBase, PesosPrimeiroDigito);
            int digito2Calculado = CalcularDigitoVerificador(
                numeroBase + digito1Calculado,
                PesosSegundoDigito
            );

            string digitosCalculados = $"{digito1Calculado}{digito2Calculado}";

            if (digitosInformados != digitosCalculados)
                return false;

            cnpj = new Cnpj(sanitizado);
            return true;
        }

        private static string Sanitizar(string entrada) =>
            Regex.Replace(entrada, @"[^A-Za-z0-9]", "").ToUpper();

        private static bool FormatoValido(string entrada) =>
            entrada.Length == 14 && RegexCnpj.IsMatch(entrada);

        private static int CalcularDigitoVerificador(string sequencia, int[] pesos)
        {
            if (sequencia.Length != pesos.Length)
                throw new ArgumentException("O comprimento da sequência não corresponde aos pesos.");

            int soma = 0;
            for (int i = 0; i < sequencia.Length; i++)
            {
                int valor = ConverterCharParaValor(sequencia[i]);
                soma += valor * pesos[i];
            }

            int resto = soma % 11;
            return (resto == 0 || resto == 1) ? 0 : 11 - resto;
        }

        private static int ConverterCharParaValor(char caractere)
        {
            int ascii = (int)char.ToUpper(caractere);
            return ascii - 48;
        }

        public string ObterValorFormatado()
        {
            if (Valor.Length != 14)
                return Valor;

            return $"{Valor.Substring(0, 2)}.{Valor.Substring(2, 3)}.{Valor.Substring(5, 3)}/" +
                   $"{Valor.Substring(8, 4)}-{Valor.Substring(12, 2)}";
        }

        public override string ToString() => Valor;
    }
}