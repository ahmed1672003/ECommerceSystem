namespace ECommerce.Domain.IRepositories;
public interface IUnitOfWork : IAsyncDisposable
{
    public ICategoryRepository Categories { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
