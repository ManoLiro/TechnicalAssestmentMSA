using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Tool.hbm2ddl;
using TechnicalAssestmentMSA.Application.Repositories;
using TechnicalAssestmentMSA.Infrastructure.Repositories;
using NHibernate;

namespace TechnicalAssestmentMSA.Infrastructure.Persistence
{
    public static class NhSessionFactory
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // 1. Configurar NHibernate com SQLite
            var sessionFactory = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard
                    .UsingFile("meubanco.db")) // Define o arquivo
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ClienteRepository>())
                .ExposeConfiguration(cfg =>
                {
                    // CRÍTICO: Isso cria as tabelas se não existirem (apenas para dev/exemplo)
                    new SchemaUpdate(cfg).Execute(false, true);
                })
                .BuildSessionFactory();

            // 2. Registrar Singleton do Factory
            services.AddSingleton(sessionFactory);

            // 3. Registrar Scoped da Sessão (uma por requisição HTTP)
            services.AddScoped<ISession>(provider =>
                provider.GetRequiredService<ISessionFactory>().OpenSession());

            // 3.1 Registrar transação por request (UoW)
            services.AddScoped<ITransaction>(sp => sp.GetRequiredService<ISession>().BeginTransaction());
            services.AddScoped<IUnitOfWorkRepository, NhUnitOfWork>();

            // 4. Registrar Repositórios
            services.AddScoped<IClienteRepository, ClienteRepository>();

            return services;
        }
    }
}
