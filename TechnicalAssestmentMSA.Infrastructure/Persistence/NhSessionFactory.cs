using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using TechnicalAssestmentMSA.Infrastructure.Persistence.Mappings;

namespace TechnicalAssestmentMSA.Infrastructure.Persistence
{
    public sealed class NhSessionFactory
    {
        public ISessionFactory SessionFactory { get; }
        public Configuration Configuracao { get; }

        public NhSessionFactory(string caminhoDoArquivoDb)
        {
            Configuration? cfgCapturada = null;

            SessionFactory = Fluently.Configure()
                .Database(
                    SQLiteConfiguration.Standard
                        .UsingFile(caminhoDoArquivoDb)
                        .ShowSql()
                )
                .Mappings(m =>
                    m.FluentMappings.AddFromAssemblyOf<ClienteMap>())
                .ExposeConfiguration(cfg => cfgCapturada = cfg)
                .BuildSessionFactory();

            Configuracao = cfgCapturada
                ?? throw new InvalidOperationException("Configuration do NHibernate não capturada.");
        }
    }
}
