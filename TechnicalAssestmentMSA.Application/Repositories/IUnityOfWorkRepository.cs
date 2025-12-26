namespace TechnicalAssestmentMSA.Application.Repositories
{
    public interface IUnityOfWorkRepository
    {
        Task CommitAsync(CancellationToken ct = default);
    }
}
