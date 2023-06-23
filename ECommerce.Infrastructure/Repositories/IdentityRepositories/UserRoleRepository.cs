namespace ECommerce.Infrastructure.Repositories.IdentityRepositories;
public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
{
    public UserRoleRepository(ECommerceDbContext context) : base(context)
    {
    }
}
