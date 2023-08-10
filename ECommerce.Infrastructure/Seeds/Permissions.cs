using ECommerce.Infrastructure.Enums;

namespace ECommerce.Infrastructure.Seeds;
public static class Permissions
{
    public static List<string> GeneratePermissionsList(string module) =>
        new()
        {
            $"Permissions.{module}.View",
            $"Permissions.{module}.Create",
            $"Permissions.{module}.Edit",
            $"Permissions.{module}.Delete",
        };

    public static List<string> GenerateAllPermissions()
    {
        var allPermissions = new List<string>();

        var modules = Enum.GetValues(typeof(Modules));

        foreach (var module in modules)
            allPermissions.AddRange(GeneratePermissionsList(module.ToString()));

        return allPermissions;
    }

    public static class Categories
    {
        public const string View = "Permissions.Categories.View";
        public const string Create = "Permissions.Categories.Create";
        public const string Edit = "Permissions.Categories.Edit";
        public const string Delete = "Permissions.Categories.Delete";
    }
}
