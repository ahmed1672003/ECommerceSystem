namespace ECommerce.Domain.IRepositories.IIdentityRepositories;

public interface IRoleRepository : IRepository<Role>
{
    public RoleManager<Role> Manager { get; }
}
