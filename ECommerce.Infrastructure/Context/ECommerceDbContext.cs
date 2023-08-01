using System.Reflection;

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
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
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
    public DbSet<UserJWT> UserJWTs { get; set; }



    #endregion
}
