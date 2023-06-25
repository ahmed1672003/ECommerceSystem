using ECommerce.Services.Helpers.AuthenticationHelpers;

using Microsoft.Extensions.Configuration;

namespace ECommerce.Services;
public static class ServicesDependencies
{
    public static IServiceCollection AddServicesDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient(typeof(IAuthenticationServices), typeof(AuthenticationServices));

        // JWT Setting
        var jwtSettings = new JwtSettings();
        configuration.GetSection(nameof(jwtSettings)).Bind(jwtSettings);
        services.AddSingleton(jwtSettings);
        return services;
    }
}
