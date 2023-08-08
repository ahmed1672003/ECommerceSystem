
using ECommerce.Infrastructure.Repositories.IdentityRepositories;

namespace ECommerce.Infrastructure;
public static class InfrastructureDependencies
{
    public static IServiceCollection AddInfrastructureDependencies
        (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<User, Role>(options =>
        {
            #region Email Options
            options.SignIn.RequireConfirmedEmail = true;
            options.SignIn.RequireConfirmedPhoneNumber = true;
            options.SignIn.RequireConfirmedAccount = true;
            #endregion

            #region Stores Options
            //options.Stores.ProtectPersonalData = true;

            #endregion

            #region Password Options
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 10;
            #endregion

            #region User Options
            options.User.RequireUniqueEmail = true;
            #endregion

            #region Lock Out Options
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;
            #endregion

            #region Claims Options

            #endregion

            #region Token Options

            #endregion

        }).AddEntityFrameworkStores<ECommerceDbContext>()
        .AddDefaultTokenProviders()
        .AddDefaultUI();

        services.AddDbContext<ECommerceDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("ECommerceConnection"));
        });
        services
            .AddScoped<ECommerceDbContext>()
            .AddScoped<ICategoryRepository, CategoryRepository>()
            .AddScoped<IUserJWTRepository, UserJWTRepository>()
            .AddScoped<IUserLoginRepository, UserLoginRepository>()
            .AddScoped<IUserRoleRepository, UserRoleRepository>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IUserTokenRepository, UserTokenRepository>()
            .AddScoped<IUserClaimRepository, UserClaimRepository>()
            .AddScoped<IRoleClaimRepository, RoleClaimRepository>()
            .AddScoped<IRoleRepository, RoleRepository>()
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<UserManager<User>>()
            .AddScoped<SignInManager<User>>()
            .AddScoped<RoleManager<Role>>();
        return services;
    }
}
