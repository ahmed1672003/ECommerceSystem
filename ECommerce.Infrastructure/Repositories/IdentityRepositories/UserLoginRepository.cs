namespace ECommerce.Infrastructure.Repositories.IdentityRepositories;
public class UserLoginRepository : Repository<UserLogin>, IUserLoginRepository
{
    public UserLoginRepository(ECommerceDbContext context) : base(context)
    {
    }
}
