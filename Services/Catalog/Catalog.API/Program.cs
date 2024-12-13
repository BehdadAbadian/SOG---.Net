using Catalog.API.Configuration;
using Catalog.API.Middleware;
using Catalog.API.MiniAPI;
using Catalog.Application.CategoryCommandQuery.Command;
using Catalog.Infrastructure;
using Serilog;
using StackExchange.Redis;
using static Catalog.API.Protos.Permission;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

Log.Information("Starting up Category Project");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddSerilog((services, lc) => lc
    .ReadFrom.Configuration(configuration)
    .ReadFrom.Services(services));

    // Add services to the container.
    builder.Services.AddGrpcClient<PermissionClient>(o =>
    {
        o.Address = new Uri(builder.Configuration["GRPC_Permission_Server_Address"]);
    });
    //builder.Services.AddLogging(logging =>
    //logging.AddSeq(builder.Configuration.GetSection("Seq")));


    builder.Services.AddInfrastructureSetup();
    builder.Services.AddDataBaseSetup(builder.Configuration);
    builder.Services.AddMemoryCache();
    //builder.Services.AddDistributedMemoryCache();

    //builder.Services.AddStackExchangeRedisCache(options =>
    //{
    //    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    //    options.InstanceName = "cache-1";

    //});


    builder.Services.AddScoped<DataValidationMiddleware>();

    builder.Services.AddMediatR(options =>
    {
        options.RegisterServicesFromAssemblies(typeof(AddCategoryCommand).Assembly);
    });


    builder.Services.AddControllers();
    builder.Services.AddOpenApi();

    var app = builder.Build();
    app.UseMiddleware<DataValidationMiddleware>();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }
    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();


    app.AddMiniAPI();

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