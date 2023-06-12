namespace ECommerce.Infrastructure.Repositories;
public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(ECommerceDbContext context) : base(context) { }

    public async Task<bool> CanCreated(string name, CancellationToken cancellationToken = default)
    {
        var category = await _entities.FirstOrDefaultAsync(e => e.Name.Equals(name));

        if (category == null)
            return false;

        return true;
    }


    public async Task<bool> CanUpdated(string name, string id)
    {
        var category = await _entities.FirstOrDefaultAsync(c => c.Name.Equals(name));
        return !(category != null && category.Id != id);
    }
}
