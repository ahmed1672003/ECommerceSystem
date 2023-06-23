namespace ECommerce.Infrastructure.Repositories.IdentityRepositories;
public class UserTokenRepository : Repository<UserToken>, IUserTokenRepository
{
    public UserTokenRepository(ECommerceDbContext context) : base(context)
    {
    }
}
