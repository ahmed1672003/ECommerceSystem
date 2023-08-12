namespace ECommerce.Infrastructure.Seeds;
public static class RolesSedeer
{
    public static async Task SeedAsync(IUnitOfWork context)
    {

        if (!await context.Roles.IsExistAsync())
        {
            await context.Roles.Manager.CreateAsync(new Role() { Name = Roles.SuperAdmin.ToString() });
            await context.Roles.Manager.CreateAsync(new Role() { Name = Roles.Admin.ToString() });
            await context.Roles.Manager.CreateAsync(new Role() { Name = Roles.Basic.ToString() });
        }
    }
}
