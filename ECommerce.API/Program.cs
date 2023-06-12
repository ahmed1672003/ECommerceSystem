using ECommerce.Application.MiddleWares;

namespace ECommerce.API;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        #region Register Services
        builder.Services
            .AddApplicationDependencies()
            .AddInfrastructureDependencies(builder.Configuration);

        builder.Services
            .AddControllers()
        // Handel Serialize loop references (but this in ASP.NetCore.OpenApi)
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

        // Allow All Cors
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
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services
            .AddAuthentication(NegotiateDefaults.AuthenticationScheme)
            .AddNegotiate();

        builder.Services.AddAuthorization(options =>
        {
            // By default, all incoming requests will be authorized according to the default policy.
            options.FallbackPolicy = options.DefaultPolicy;
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseSwagger();
        app.UseSwaggerUI();

        #region Use Services
        app.UseCors("ECommerce");

        app.UseMiddleware<ErrorHandlerMiddleWare>();
        #endregion

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
