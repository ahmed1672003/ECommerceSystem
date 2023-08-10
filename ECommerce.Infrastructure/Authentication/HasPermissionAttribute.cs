using Microsoft.AspNetCore.Authorization;

namespace ECommerce.Infrastructure.Filters;
public class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string permission)
        : base(policy: permission)
    {

    }
}
