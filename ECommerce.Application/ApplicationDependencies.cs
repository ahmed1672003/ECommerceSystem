using System.ComponentModel.DataAnnotations;

using ECommerce.Application.Behaviors.ValidatorBehavior;

namespace ECommerce.Application;
public static class ApplicationDependencies
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IResponseHandler), typeof(ResponseHandler));
        services.AddTransient(typeof(IResponseHandler), typeof(PaginationResponseHandler));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient<EmailAddressAttribute>();
        return services;
    }
}
