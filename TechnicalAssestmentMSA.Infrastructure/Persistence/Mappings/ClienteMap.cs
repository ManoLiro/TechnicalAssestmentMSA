using FluentNHibernate.Mapping;
using TechnicalAssestmentMSA.Domain.Entidades;

namespace TechnicalAssestmentMSA.Infrastructure.Persistence.Mappings
{
    public sealed class ClienteMap : ClassMap<Cliente>
    {
        public ClienteMap()
        {
            Table("clientes");

            Id(x => x.Id)
                .GeneratedBy.Assigned();

            Map(x => x.NomeFantasia).Not.Nullable().Length(200);
            Component(x => x.Cnpj, c =>
            {
                c.Map(x => x.Valor)
                 .Column("cnpj")
                 .Not.Nullable()
                 .Length(14);
            });
            Map(x => x.Ativo).Not.Nullable();
        }
    }
}
