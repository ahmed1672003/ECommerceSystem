namespace ECommerce.Domain.IRepositories.IIdentityRepositories;
public interface IUserRepository : IRepository<User>
{
    public UserManager<User> Manager { get; }
}
