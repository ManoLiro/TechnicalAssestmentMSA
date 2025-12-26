namespace TechnicalAssestmentMSA.Application.Repositories
{
    public interface IUnidadeDeTrabalho
    {
        Task CommitAsync(CancellationToken ct = default);
    }
}
