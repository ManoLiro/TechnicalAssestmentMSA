using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace TechnicalAssestmentMSA.Infrastructure.Persistence
{
    public static class NhSchema
    {
        public static void CriarSchema(Configuration cfg)
        {
            new SchemaExport(cfg).Create(useStdOut: true, execute: true);
        }
    }
}
