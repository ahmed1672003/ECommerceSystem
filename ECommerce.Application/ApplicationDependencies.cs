using System.ComponentModel.DataAnnotations;

using ECommerce.Application.Behaviors.ValidatorBehavior;

namespace ECommerce.Application;
public static class ApplicationDependencies
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddScoped<IResponseHandler, ResponseHandler>();
        services.AddScoped<IResponseHandler, PaginationResponseHandler>();
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddScoped<EmailAddressAttribute>();



        return services;
    }
}
