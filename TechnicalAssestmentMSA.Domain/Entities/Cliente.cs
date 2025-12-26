using TechnicalAssestmentMSA.Domain.ValueObjects;

namespace TechnicalAssestmentMSA.Domain.Entidades
{
    public class Cliente
    {
        public Cliente(string nomeFantasia, Cnpj cnpj, bool ativo)
        {
            if (String.IsNullOrEmpty(nomeFantasia))
                throw new ArgumentNullException("Nome Fantasia não deve ser nulo ou vazio");
            else
                NomeFantasia = nomeFantasia;

            Cnpj = cnpj;
            Ativo = ativo;
        }

        public Guid Id { get; private set; }
        public string NomeFantasia { get; private set; }
        public Cnpj Cnpj { get; private set; }
        public bool Ativo { get; private set; }


    }
}
