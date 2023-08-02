using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.OpenApi.Models;

namespace ECommerce.Services;
public static class ServicesDependencies
{
    public static IServiceCollection AddServicesDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        #region Register Service
        services
            .AddHttpContextAccessor()
            .AddScoped<IAuthenticationService, AuthenticationService>()
            .AddScoped<ICookieService, CookieService>()
            .AddScoped<IIPInfoService, IPInfoService>()
            .AddScoped<IUnitOfServices, UnitOfServices>()
            .AddScoped<ISessionService, SessionService>()
            .AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        #endregion

        #region JWT Services
        services
            .AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {

                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration.GetValue<string>($"{nameof(JwtSettings)}:{nameof(JwtSettings.Issuer)}"),
                    ValidAudience = configuration.GetValue<string>($"{nameof(JwtSettings)}:{nameof(JwtSettings.Audience)}"),
                    IssuerSigningKey =
                    new SymmetricSecurityKey(
                    Encoding.UTF8.
                    GetBytes(configuration.GetValue<string>($"{nameof(JwtSettings)}:{nameof(JwtSettings.Secret)}"))
                    ),
                };
            });

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new() { Title = "E~Commerce", Version = "v1" });
            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new()
            {
                Description = "JWT Authorization",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = JwtBearerDefaults.AuthenticationScheme
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme()
                        {
                            Reference = new()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                    Array.Empty<string>()
                }
            });
        });

        services
            .Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
        #endregion

        return services;
    }
}
