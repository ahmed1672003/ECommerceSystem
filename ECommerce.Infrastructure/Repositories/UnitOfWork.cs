namespace ECommerce.Infrastructure.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly ECommerceDbContext _context;

    public UnitOfWork(ECommerceDbContext context)
    {
        _context = context;
        Categories = new CategoryRepository(_context);
    }
    public ICategoryRepository Categories { get; private set; }

    public async ValueTask DisposeAsync() => await _context.DisposeAsync();

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);

}
