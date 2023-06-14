namespace ECommerce.Domain.IRepositories;
public interface ICategoryRepository : IRepository<Category>
{
    Task<bool> CanCreatedAsync(string name, CancellationToken cancellationToken = default);
    Task<bool> CanUpdatedAsync(string name, string id);
}
