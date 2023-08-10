namespace ECommerce.Infrastructure.Context;
public interface IECommerceDbContext
{
    DbSet<Category> Categories { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<Role> Roles { get; set; }
    DbSet<RoleClaim> RoleClaims { get; set; }
    DbSet<UserClaim> UserClaims { get; set; }
    DbSet<UserLogin> UserLogins { get; set; }
    DbSet<UserRole> UserRoles { get; set; }
    DbSet<UserToken> UserTokens { get; set; }
    DbSet<UserJWT> UserJWTs { get; set; }
}
