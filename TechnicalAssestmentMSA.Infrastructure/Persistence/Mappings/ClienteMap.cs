using FluentNHibernate.Mapping;
using NHibernate.Id;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalAssestmentMSA.Domain.Entidades;

namespace TechnicalAssestmentMSA.Infrastructure.Persistence.Mappings
{
    public sealed class ClienteMap : ClassMap<Cliente>
    {
        public ClienteMap()
        {
            Table("clientes");

            Id(x => x.Id)
                .GeneratedBy.Guid();

            Map(x => x.NomeFantasia).Not.Nullable().Length(200);
            Map(x => x.Cnpj).Not.Nullable().Length(14);
            Map(x => x.Ativo).Not.Nullable().CustomType<Boolean>();
        }
    }
}
