using TechnicalAssestmentMSA.Domain.ValueObjects;

namespace TechnicalAssestmentMSA.Domain.Entidades
{
    public class Cliente
    {
        public Guid Id { get; private set; }
        public string NomeFantasia { get; private set; } = default!;
        public Cnpj Cnpj { get; private set; } = default!;
        public bool Ativo { get; private set; }

        private Cliente() { } // NHibernate

        public Cliente(Guid id, string nomeFantasia, Cnpj cnpj, bool ativo = true)
        {
            if (id == Guid.Empty)
                throw new Exception("Id do cliente é inválido.");

            AlterarNomeFantasia(nomeFantasia);

            Cnpj = cnpj ?? throw new Exception("CNPJ é obrigatório.");
            Id = id;
            Ativo = ativo;
        }

        public void AlterarNomeFantasia(string nomeFantasia)
        {
            if (string.IsNullOrWhiteSpace(nomeFantasia))
                throw new Exception("Nome fantasia é obrigatório.");

            NomeFantasia = nomeFantasia.Trim();
        }

        public void Ativar() => Ativo = true;
        public void Desativar() => Ativo = false;

    }
}
