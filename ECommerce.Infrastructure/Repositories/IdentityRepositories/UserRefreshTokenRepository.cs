namespace ECommerce.Infrastructure.Repositories.IdentityRepositories;
public class UserRefreshTokenRepository : Repository<UserRefreshToken>, IUserRefreshTokenRepository
{
    public UserRefreshTokenRepository(ECommerceDbContext context) : base(context) { }


}
