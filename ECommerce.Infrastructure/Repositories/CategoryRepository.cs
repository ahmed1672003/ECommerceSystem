namespace ECommerce.Infrastructure.Repositories;
public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(ECommerceDbContext context) : base(context) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> CanCreatedAsync(string name, CancellationToken cancellationToken = default)
    {
        var category = await _entities.FirstOrDefaultAsync(e => e.Name.Equals(name));

        if (category == null)
            return false;
        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> CanUpdatedAsync(string name, string id, CancellationToken cancellationToken = default)
    {
        var category = await _entities.FirstOrDefaultAsync(c => c.Name.Equals(name), cancellationToken);
        return !(category != null && category.Id != id);
    }
}
