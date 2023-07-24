﻿using Microsoft.OpenApi.Models;

namespace ECommerce.Services;
public static class ServicesDependencies
{
    public static IServiceCollection AddServicesDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        #region JWT Services
        // JWT Setting
        services.AddSingleton(typeof(JWT));
        services.Configure<JWT>(configuration.GetSection(nameof(JWT)));
        services.AddAuthentication(options =>
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
                    ValidIssuer = configuration.GetValue<string>($"{nameof(JWT)}:{nameof(JWT.Issuer)}"),
                    ValidAudience = configuration.GetValue<string>($"{nameof(JWT)}:{nameof(JWT.Audience)}"),
                    IssuerSigningKey =
                    new SymmetricSecurityKey(
                    Encoding.UTF8.
                    GetBytes(configuration.GetValue<string>($"{nameof(JWT)}:{nameof(JWT.Key)}"))
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
        services.AddScoped<IAuthService, AuthService>();
        #endregion  

        return services;
    }
}
