using Microsoft.Extensions.DependencyInjection;
using Security.API.Configurations;
using Security.API.Services;
using Security.Application;
using Security.Application.Contracts.Interface;
using Security.Application.Permission;
using Security.Application.User.Command;
using Security.Domain.User;
using Security.Infrastructure.Database;
using Security.Infrastructure.Pattern;
using Security.Infrastructure.Repository;
using Security.Infrastructure.Utility.Encryption;
using Security.Infrastructure.Utility.Model;
using Serilog;
using static Security.API.Protos.Permission;



var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

Log.Information("Starting up Security Project");

try
{
    var builder = WebApplication.CreateBuilder(args);



    builder.Services.AddSerilog((services, lc) => lc
        .ReadFrom.Configuration(configuration)
        .ReadFrom.Services(services));

    // Add services to the container.
    builder.Services.AddOptions();
    builder.Services.Configure<Configs>(builder.Configuration.GetSection("Configs"));

    builder.Services.AddMediatR(o =>
    {
        o.RegisterServicesFromAssembly(typeof(SaveUserCommand).Assembly);
    });
    builder.Services.AddGrpc();
    builder.Services.AddControllers();
    builder.Services.AddMemoryCache();
    builder.Services.AddOpenApi();
    builder.Services.AddDataBaseSetup(builder.Configuration);
    Security.Infrastructure.InfrastructureSetup.AddInfrastructure(builder.Services);
    builder.Services.AddScoped<EncryptionUtility>();
    builder.Services.AddScoped<IPermissionApplicationService, PermissionApplicationService>();
    builder.Services.AddJWT();
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseHttpsRedirection();
    app.MapControllers();
    app.UseRouting();
    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapGrpcService<PermissionService>();
    });

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}