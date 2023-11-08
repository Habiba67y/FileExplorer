namespace FileExplorer.Api.Configurations;

public static partial class HostConfiguration
{
    public static ValueTask<WebApplicationBuilder> ConfigureAsync(this WebApplicationBuilder builder)
    {
        builder
            .AddAutoMapper()
            .AddSettings()
            .AddServices()
            .AddCors()
            .AddDevTools()
            .AddExposers();

        return new(builder);
    }

    public static ValueTask<WebApplication> ConfigureAsync(this WebApplication app)
    {
        app
            .UseDevTools()
            .UseExposers()
            .UseCors("CorsPolicy");

        return new(app);
    }
}
