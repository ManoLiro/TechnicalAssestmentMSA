using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Tool.hbm2ddl;
using TechnicalAssestmentMSA.Application.Repositories;
using TechnicalAssestmentMSA.Infrastructure.Repositories;
using NHibernate;
using NHibernate.Driver;  // <--- Importante para acessar o MicrosoftDataSqliteDriver
using NHibernate.Dialect; // <--- Importante para o Dialeto

namespace TechnicalAssestmentMSA.Infrastructure.Persistence
{
    public static class NhSessionFactory
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // 1. Configurar NHibernate com SQLite usando Microsoft.Data.Sqlite
                var sessionFactory = Fluently.Configure().Database(MsSqliteConfiguration.Standard.ConnectionString(c => c.Is($"data source=meubanco.db;")).Dialect<NHibernate.Extensions.Sqlite.SqliteDialect>().Driver<NHibernate.Extensions.Sqlite.SqliteDriver>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ClienteRepository>())
                .ExposeConfiguration(cfg =>
                {
                    // CRÍTICO: SchemaUpdate para dev
                    new SchemaUpdate(cfg).Execute(false, true);
                })
                .BuildSessionFactory();

            // 2. Registrar Singleton do Factory
            services.AddSingleton(sessionFactory);

            // 3. Registrar Scoped da Sessão
            services.AddScoped<ISession>(provider =>
                provider.GetRequiredService<ISessionFactory>().OpenSession());

            // 3.1 Registrar transação e UoW
            // NOTA: Cuidado ao injetar ITransaction diretamente (veja observação abaixo)
            services.AddScoped<ITransaction>(sp => sp.GetRequiredService<ISession>().BeginTransaction());
            services.AddScoped<IUnitOfWorkRepository, NhUnitOfWork>();

            // 4. Registrar Repositórios
            services.AddScoped<IClienteRepository, ClienteRepository>();

            return services;
        }
    }
}