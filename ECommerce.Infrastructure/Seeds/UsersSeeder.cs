using ECommerce.Domain.Enums.Claim;
using ECommerce.Infrastructure.Enums;

namespace ECommerce.Infrastructure.Seeds;
public static class UsersSeeder
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static async Task SeedSuperAdminUserAsync(IUnitOfWork context)
    {
        var superAdminUser = new User
        {
            UserName = "superadmin",
            Email = "ahmedadel1672003@gmail.com",
            PhoneNumber = "01018856093",
            EmailConfirmed = true,
        };

        if (!await context.Users.IsExistAsync(u => u.Email.Equals(superAdminUser.Email)))
        {
            await context.Users.Manager.CreateAsync(superAdminUser, "Ahmed#01280755031");
            await context.Users.Manager.AddToRolesAsync(superAdminUser, new List<string>
                {
                    Roles.SuperAdmin.ToString(),
                    Roles.Admin.ToString(),
                    Roles.Basic.ToString(),
                });
        }

        // Assign Super Admin Permissions
        await context.Roles.Manager.SeedClaimsForSuperAdminAsync();


    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static async Task SeedAdminUserAsync(IUnitOfWork context)
    {
        var adminUser = new User
        {
            UserName = "admin",
            Email = "ahmedadel112019@gmail.com",
            PhoneNumber = "01018856093",
            EmailConfirmed = true,
        };

        if (!await context.Users.IsExistAsync(u => u.Email.Equals(adminUser.Email)))
        {
            await context.Users.Manager.CreateAsync(adminUser, "Ahmed#01280755031");
            await context.Users.Manager.AddToRolesAsync(adminUser, new List<string>
                {
                    Roles.Admin.ToString(),
                    Roles.Basic.ToString(),
                });
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static async Task SeedBasicUserAsync(IUnitOfWork context)
    {
        var basicUser = new User
        {
            UserName = "basicuser",
            Email = "ahmedadel1122003@gmail.com",
            PhoneNumber = "01018856093",
            EmailConfirmed = true,
        };

        if (!await context.Users.IsExistAsync(u => u.Email.Equals(basicUser.Email)))
        {
            await context.Users.Manager.CreateAsync(basicUser, "Ahmed#01280755031");
            await context.Users.Manager.AddToRoleAsync(basicUser, Roles.Basic.ToString());
        }

    }

    private static async Task SeedClaimsForSuperAdminAsync(this RoleManager<Role> roleManager)
    {
        var superAdminRole = await roleManager.FindByNameAsync(Roles.SuperAdmin.ToString());
        await roleManager.AddPermissionClaimsAsync(superAdminRole, Modules.Categories.ToString());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="manager"></param>
    /// <param name="role"></param>
    /// <param name="module"></param>
    /// <returns></returns>
    private static async Task AddPermissionClaimsAsync(this RoleManager<Role> roleManager, Role role, string module)
    {
        // get claims for role
        var roleClaims = await roleManager.GetClaimsAsync(role);

        // generate permissions 
        var allModulePermissions = Permissions.GeneratePermissionsList(module);

        // add all claims to role and add permissions to claim
        foreach (var permission in allModulePermissions)
            if (!roleClaims.Any(c => c.Type.Equals(CustomClaims.Permission.ToString()) && c.Value.Equals(permission)))
                await roleManager.AddClaimAsync(role, new(CustomClaims.Permission.ToString(), permission));
    }
}
