using Application.Brokers;
using Application.Services;
using Infrastructure.Brokers;
using Infrastructure.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load).ToList();
assemblies.Add(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(assemblies);

builder.Services
    .AddSingleton<IDriveBroker, DriveBroker>()
    .AddSingleton<IDirectoryBroker, DirectoryBroker>()
    .AddSingleton<IFileBroker, FileBroker>()
    .AddSingleton<IFileService, FileService>()
    .AddSingleton<IDirectoryService, DirectoryService>()
    .AddSingleton<IEntryService, EntryService>()
    .AddSingleton<IDriveService, DriveService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policyBuilder => { policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
});

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddSwaggerGen().AddEndpointsApiExplorer(); 
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseStaticFiles();
app.UseSwagger().UseSwaggerUI();
app.UseCors("CorsPolicy");
app.MapControllers();

app.Run();
