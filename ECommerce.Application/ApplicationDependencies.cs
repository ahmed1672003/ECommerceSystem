﻿using System.ComponentModel.DataAnnotations;

using ECommerce.Application.Behaviors.ValidatorBehavior;
using ECommerce.Application.ResponseServices;

namespace ECommerce.Application;
public static class ApplicationDependencies
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        #region Add Services
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddScoped<IResponseHandler, ResponseHandler>();
        services.AddScoped<IResponseHandler, PaginationResponseHandler>();
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddScoped<EmailAddressAttribute>();
        #endregion

        return services;
    }
}
