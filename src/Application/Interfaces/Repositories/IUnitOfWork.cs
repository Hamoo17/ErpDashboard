using ErpDashboard.Domain.Contracts;

namespace ErpDashboard.Application.Interfaces.Repositories
{
    public interface IUnitOfWork<TId> : IDisposable
    {
        IRepositoryAsync<T, TId> Repository<T>() where T : AuditableEntity<TId>;

        Task<int> Commit(CancellationToken cancellationToken);

        Task<int> CommitAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys);

        Task Rollback();
    }
    public interface ICustomIUnitOfWork<TId> : IDisposable
    {
        CustomIRepositoryAsync<T, TId> Repository<T>() where T : class;

        Task<int> Commit(CancellationToken cancellationToken);

        Task<int> CommitAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys);

        Task Rollback();
    }
}