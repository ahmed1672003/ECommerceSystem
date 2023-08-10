using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace ECommerce.Infrastructure.Filters;
public class PermissionPolicyProvider : IAuthorizationPolicyProvider
{
    public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }

    public PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
    {
        FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
    }

    public async Task<AuthorizationPolicy> GetDefaultPolicyAsync() =>
        await FallbackPolicyProvider.GetDefaultPolicyAsync();

    public async Task<AuthorizationPolicy> GetFallbackPolicyAsync() =>
         await FallbackPolicyProvider.GetDefaultPolicyAsync();

    public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
    {
        if (policyName.StartsWith("Permission", StringComparison.OrdinalIgnoreCase))
        {
            var policy = new AuthorizationPolicyBuilder();
            policy.AddRequirements(new PermissionRequirement(policyName));
            return Task.FromResult(policy.Build());
        }
        return FallbackPolicyProvider.GetPolicyAsync(policyName);
    }
}
