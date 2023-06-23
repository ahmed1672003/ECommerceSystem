using ECommerce.Infrastructure.Context.Configurations.IdentityConfigurations;

namespace ECommerce.Infrastructure.Context;
public class ECommerceDbContext :
    IdentityDbContext<User, Role, string, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
{
    public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        new CategoryConfigurations().Configure(modelBuilder.Entity<Category>());
        new UserConfigurations().Configure(modelBuilder.Entity<User>());
        new RoleClaimConfigurations().Configure(modelBuilder.Entity<RoleClaim>());
        new UserClaimConfigurations().Configure(modelBuilder.Entity<UserClaim>());
        new UserRoleConfigurations().Configure(modelBuilder.Entity<UserRole>());
        new UserLoginConfigurations().Configure(modelBuilder.Entity<UserLogin>());
        new RoleClaimConfigurations().Configure(modelBuilder.Entity<RoleClaim>());
        new UserTokenConfigurations().Configure(modelBuilder.Entity<UserToken>());
    }
    public DbSet<Category> Categories { get; set; }
}
