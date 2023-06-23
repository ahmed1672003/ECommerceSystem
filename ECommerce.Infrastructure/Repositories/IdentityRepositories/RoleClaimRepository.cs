namespace ECommerce.Infrastructure.Repositories.IdentityRepositories;
public class RoleClaimRepository : Repository<RoleClaim>, IRoleClaimRepository
{
    public RoleClaimRepository(ECommerceDbContext context) : base(context) { }
}
