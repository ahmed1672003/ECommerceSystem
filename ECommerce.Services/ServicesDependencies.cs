

namespace ECommerce.Services;
public static class ServicesDependencies
{
    public static IServiceCollection AddServicesDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient(typeof(IAuthenticationServices), typeof(AuthenticationServices));


        #region JWT Services
        // JWT Setting
        var jwtSettings = new JwtSettings();
        services.AddSingleton(jwtSettings);
        configuration.GetSection(nameof(jwtSettings)).Bind(jwtSettings);

        services.AddAuthentication(cfg =>
        {
            cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(cfg =>
        {
            cfg.RequireHttpsMetadata = false;
            cfg.SaveToken = true;
            cfg.TokenValidationParameters = new()
            {
                ValidateIssuer = jwtSettings.ValidateIssuer,
                ValidIssuers = { jwtSettings.Issuer },

            };
        });





        #endregion

        return services;
    }
}
