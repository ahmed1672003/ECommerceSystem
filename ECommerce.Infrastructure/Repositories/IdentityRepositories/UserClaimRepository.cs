namespace ECommerce.Infrastructure.Repositories.IdentityRepositories;
public class UserClaimRepository : Repository<UserClaim>, IUserClaimRepository
{
    public UserClaimRepository(ECommerceDbContext context) : base(context)
    {
    }
}
