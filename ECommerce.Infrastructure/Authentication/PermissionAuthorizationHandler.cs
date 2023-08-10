using ECommerce.Infrastructure.Settings;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace ECommerce.Infrastructure.Authentication;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly JwtSettings _jwtSettings;
    private readonly IUnitOfWork _context;
    public PermissionAuthorizationHandler(IOptions<JwtSettings> options, IUnitOfWork context)
    {
        _jwtSettings = options.Value;
        _context = context;
    }

    protected override async Task HandleRequirementAsync
        (AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        if (context.User == null)
            return;

        var canAccess = context.User.Claims.Any(c => c.Type == "Permission" && c.Value == requirement.Permission && c.Issuer == _jwtSettings.Issuer);

        if (canAccess)
        {
            context.Succeed(requirement);
            return;
        }
    }
}
