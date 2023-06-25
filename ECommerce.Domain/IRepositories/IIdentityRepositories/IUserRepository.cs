namespace ECommerce.Domain.IRepositories.IIdentityRepositories;
public interface IUserRepository : IRepository<User>
{
    UserManager<User> Manager { get; }
    SignInManager<User> SignInManager { get; }
}
