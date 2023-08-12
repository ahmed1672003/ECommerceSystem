using ECommerce.Application.MiddleWares;
using ECommerce.Domain.Enums.Claim;
using ECommerce.Domain.IRepositories;
using ECommerce.Infrastructure.Seeds;
using ECommerce.Services;

namespace ECommerce.API;

public class Startup
{
    public static async Task BuildServices(WebApplicationBuilder builder)
    {

        // Add services to the container.
        #region Register Custom Services

        #region Custom Dependencies
        builder.Services
           .AddApplicationDependencies()
           .AddInfrastructureDependencies(builder.Configuration)
           .AddServicesDependencies(builder.Configuration);
        #endregion

        #region Seed Default Data
        var loggerFactory = builder.Services.BuildServiceProvider().GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger("app");
        var context = builder.Services.BuildServiceProvider().GetRequiredService<IUnitOfWork>();
        try
        {
            await RolesSedeer.SeedAsync(context);
            await UsersSeeder.SeedSuperAdminUserAsync(context);
            await UsersSeeder.SeedAdminUserAsync(context);
            await UsersSeeder.SeedBasicUserAsync(context);
            logger.LogInformation("Default Data Seeded Successfully");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding data", ex.Message);
        }

        #endregion

        #region Handel Serialize loop references
        builder.Services
            .AddControllers()
        // Handel Serialize loop references (but this in ASP.NetCore.OpenApi)
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
        #endregion

        #region Allow All Cors Origin
        builder.Services
            .AddCors(options =>
            {
                options.AddPolicy("ECommerce", builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                });
            });
        #endregion

        #region Add Localization
        //builder.Services.AddControllersWithViews();
        //builder.Services
        //    .AddLocalization(options =>
        //    {
        //        options.ResourcesPath = string.Empty;
        //    });

        //builder.Services
        //    .Configure<RequestLocalizationOptions>(options =>
        //    {
        //        IList<CultureInfo> supportedCultures = new List<CultureInfo>
        //        {
        //            new ("en-US"),
        //            new ("de-DE"),
        //            new ("fr-FR"),
        //            new ("en-GB"),
        //            new ("ar-EG"),
        //        };

        //        options.DefaultRequestCulture = new("ar-EG");
        //        options.SupportedCultures = supportedCultures;
        //        options.SupportedUICultures = supportedCultures;
        //    });
        #endregion

        #region Add Authorization Policy
        builder.Services.AddAuthorization(o =>
        {
            o.AddPolicy(Permissions.Categories.Create, p =>
            {
                p.RequireClaim(CustomClaims.Permission.ToString());
            });
        });
        #endregion

        #endregion

        #region Built In Services

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services
            .AddAuthentication(NegotiateDefaults.AuthenticationScheme)
            .AddNegotiate();

        #endregion

    }

    public static void UseServices(WebApplication app)
    {
        #region Use Services
        app.UseSwagger();
        app.UseSwaggerUI();

        #region Use Cors
        app.UseCors("ECommerce");
        #endregion

        #region Validation MaiddleWare
        app.UseMiddleware<ErrorHandlerMiddleWare>();
        #endregion

        #region Use Localization Middle Ware
        //var options = app.Services.GetService<RequestLocalizationOptions>();
        //app.UseRequestLocalization(options =>
        //{
        //    // To Do
        //});
        #endregion

        app.UseAuthentication();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        #endregion

    }
}
