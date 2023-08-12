namespace ECommerce.API;
public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        await Startup.BuildServices(builder);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        Startup.UseServices(app);
        app.Run();
    }
}
