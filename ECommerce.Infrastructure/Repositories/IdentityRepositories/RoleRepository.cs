using Microsoft.AspNetCore.Identity;

namespace ECommerce.Infrastructure.Repositories.IdentityRepositories;
public class RoleRepository : Repository<Role>, IRoleRepository
{
    public RoleRepository(ECommerceDbContext context, RoleManager<Role> manager) : base(context)
    {
        Manager = manager;
    }
    public RoleManager<Role> Manager { get; private set; }
}
