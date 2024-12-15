using Microsoft.Extensions.DependencyInjection;
using Security.API.Services;
using Security.Domain.User;
using Security.Infrastructure.Database;
using Security.Infrastructure.Pattern;
using Security.Infrastructure.Repository;
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
    builder.Services.AddGrpc();
    builder.Services.AddControllers();
    builder.Services.AddOpenApi();
    builder.Services.AddDataBaseSetup(builder.Configuration);
    Security.Infrastructure.InfrastructureSetup.AddInfrastructure(builder.Services);
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