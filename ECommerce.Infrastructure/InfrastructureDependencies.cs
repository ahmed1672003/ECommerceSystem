namespace ECommerce.Infrastructure;
public static class InfrastructureDependencies
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ECommerceDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("ECommerceConnection"));
        });
        services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
        return services;
    }
}
