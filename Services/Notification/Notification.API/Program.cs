using Notification.API.BackgroundServices;
using Notification.Application;
using Notification.Application.Contracts.Share;
using Notification.Application.Email.CQRS.Command;
using Notification.Infrastructure;
using Notification.Infrastructure.Database;
using Serilog;

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

    // Add services to the container.
    builder.Services.AddHostedService<EmailConsumerHostedService>();


    builder.Services.AddOptions();
    builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection("EmailConfiguration"));

    builder.Services.AddDataBaseSetup(builder.Configuration);
    builder.Services.AddInfrastructure();
    builder.Services.AddServiceSetup();
    

    builder.Services.AddMediatR(o => 
    {
        o.RegisterServicesFromAssemblyContaining(typeof(SaveEmailCommand));
    });
 
    builder.Services.AddControllers();
    builder.Services.AddOpenApi();
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

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