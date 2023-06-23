namespace ECommerce.Infrastructure.Context;
public class ECommerceDbContext : IdentityDbContext
{
    public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        new CategoryConfigurations().Configure(modelBuilder.Entity<Category>());
    }
    public DbSet<Category> Categories { get; set; }
}
