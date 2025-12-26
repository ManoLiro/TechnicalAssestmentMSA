using TechnicalAssestmentMSA.Domain.ValueObjects;

namespace TechnicalAssestmentMSA.Domain.Entidades
{
    public class Cliente
    {
        public virtual Guid Id { get; protected set; }
        public virtual string NomeFantasia { get; protected set; } = default!;
        public virtual Cnpj Cnpj { get; protected set; } = default!;
        public virtual bool Ativo { get; protected set; }

        protected Cliente() { } // NHibernate proxy

        public Cliente(Guid id, string nomeFantasia, Cnpj cnpj, bool ativo = true)
        {
            if (id == Guid.Empty)
                throw new Exception("Id do cliente é inválido.");

            AlterarNomeFantasia(nomeFantasia);

            Cnpj = cnpj ?? throw new Exception("CNPJ é obrigatório.");
            Id = id;
            Ativo = ativo;
        }

        public virtual void AlterarNomeFantasia(string nomeFantasia)
        {
            if (string.IsNullOrWhiteSpace(nomeFantasia))
                throw new Exception("Nome fantasia é obrigatório.");

            NomeFantasia = nomeFantasia.Trim();
        }

        public virtual void Ativar() => Ativo = true;
        public virtual void Desativar() => Ativo = false;
    }
}
