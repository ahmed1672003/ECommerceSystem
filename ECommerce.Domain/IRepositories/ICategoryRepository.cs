namespace ECommerce.Domain.IRepositories;
public interface ICategoryRepository : IRepository<Category>
{
    Task<bool> CanCreated(string name, CancellationToken cancellationToken = default);
    Task<bool> CanUpdated(string name, string id);
}
