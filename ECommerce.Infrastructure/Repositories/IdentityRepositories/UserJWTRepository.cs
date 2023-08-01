namespace ECommerce.Infrastructure.Repositories.IdentityRepositories;
public class UserJWTRepository : Repository<UserJWT>, IUserJWTRepository
{
    public UserJWTRepository(ECommerceDbContext context) : base(context) { }
}
