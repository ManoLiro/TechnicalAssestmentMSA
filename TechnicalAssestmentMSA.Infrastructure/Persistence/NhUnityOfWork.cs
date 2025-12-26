using NHibernate;
using TechnicalAssestmentMSA.Application.Repositories;

namespace TechnicalAssestmentMSA.Infrastructure.Persistence
{
    public sealed class NhUnityOfWork : IUnityOfWorkRepository
    {
        private readonly ITransaction _transacao;

        public NhUnityOfWork(ITransaction transacao)
            => _transacao = transacao;

        public Task CommitAsync(CancellationToken ct = default)
        {
            _transacao.Commit();
            return Task.CompletedTask;
        }
    }
}
