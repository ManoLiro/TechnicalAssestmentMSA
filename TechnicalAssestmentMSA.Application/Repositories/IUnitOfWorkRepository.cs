namespace TechnicalAssestmentMSA.Application.Repositories
{
    public interface IUnitOfWorkRepository
    {
        Task CommitAsync(CancellationToken ct = default);
    }
}
