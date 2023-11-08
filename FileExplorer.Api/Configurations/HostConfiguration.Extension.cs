using Application.Brokers;
using Application.Services;
using Application.Settings;
using Infrastructure.Brokers;
using Infrastructure.Services;
using System.Reflection;
using System.Text;

namespace FileExplorer.Api.Configurations;

public static partial class HostConfiguration
{
    private static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder
            .Services
            .AddSingleton<IDriveBroker, DriveBroker>()
            .AddSingleton<IDirectoryBroker, DirectoryBroker>()
            .AddSingleton<IFileBroker, FileBroker>()
            .AddSingleton<IFileService, FileService>()
            .AddSingleton<IDirectoryService, DirectoryService>()
            .AddSingleton<IDirectoryProcessingService, DirectoryProcessingService>()
            .AddSingleton<IFileProcessingService, FileProcessingService>()
            .AddSingleton<IDriveService, DriveService>();

        return builder;
    }

    private static WebApplicationBuilder AddAutoMapper(this WebApplicationBuilder builder)
    {
        var assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load).ToList();
        assemblies.Add(Assembly.GetExecutingAssembly());
        builder.Services.AddAutoMapper(assemblies);

        return builder;
    }

    private static WebApplicationBuilder AddSettings(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<FileExtensionSettings>(builder.Configuration.GetSection(nameof(FileExtensionSettings)));
        builder.Services.Configure<FileFilterSettings>(builder.Configuration.GetSection(nameof(FileFilterSettings)));
        builder.Services.Configure<FileStorageSettings>(builder.Configuration.GetSection(nameof(FileStorageSettings)));

        return builder;
    }

    private static WebApplicationBuilder AddCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", policyBuilder => { policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
        });

        return builder;
    }
    private static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen();
        builder.Services.AddEndpointsApiExplorer();

        return builder;
    }
    
    private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddControllers().AddNewtonsoftJson(); 

        return builder;
    }
    private static WebApplication UseDevTools(this WebApplication app)
    {
        app.UseStaticFiles();
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }

    private static WebApplication UseExposers(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }

}
