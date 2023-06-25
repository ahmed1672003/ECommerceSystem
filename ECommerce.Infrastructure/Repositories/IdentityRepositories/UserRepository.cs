namespace ECommerce.Infrastructure.Repositories.IdentityRepositories;
public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(
        ECommerceDbContext context,
        UserManager<User> manager,
        SignInManager<User> signInManager) : base(context)
    {
        Manager = manager;
        SignInManager = signInManager;
    }

    public UserManager<User> Manager { get; }
    public SignInManager<User> SignInManager { get; }
}
