using ECommerce.Infrastructure.Context.Configurations.IdentityConfigurations;

namespace ECommerce.Infrastructure.Context;
public class ECommerceDbContext :
    IdentityDbContext<User, Role, string, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
{
    #region Constructors
    public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options) { }
    #endregion

    #region Bahaviors
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

    #endregion

    #region Properties
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<RoleClaim> RoleClaims { get; set; }
    public DbSet<UserClaim> UserClaims { get; set; }
    public DbSet<UserLogin> UserLogins { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<UserToken> UserTokens { get; set; }
    #endregion
}
