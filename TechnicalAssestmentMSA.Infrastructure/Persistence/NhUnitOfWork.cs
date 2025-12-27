using NHibernate;
using TechnicalAssestmentMSA.Application.Repositories;

namespace TechnicalAssestmentMSA.Infrastructure.Persistence
{
    public sealed class NhUnitOfWork : IUnitOfWorkRepository
    {
        private readonly ITransaction _transacao;

        public NhUnitOfWork(ITransaction transacao)
            => _transacao = transacao;

        public Task CommitAsync(CancellationToken ct = default)
        {
            _transacao.Commit();
            return Task.CompletedTask;
        }
    }
}
