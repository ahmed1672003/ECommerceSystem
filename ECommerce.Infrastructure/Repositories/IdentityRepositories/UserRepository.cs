using Microsoft.AspNetCore.Identity;

namespace ECommerce.Infrastructure.Repositories.IdentityRepositories;
public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ECommerceDbContext context, UserManager<User> manager) : base(context)
    {
        Manager = manager;
    }

    public UserManager<User> Manager { get; }
}
