using Azure.Core;
using Catalog.API.Configuration;
using Catalog.API.Middleware;
using Catalog.API.MiniAPI;
using Catalog.Application.CategoryCommandQuery.Command;
using Catalog.Application.CategoryCommandQuery.Query;
using Catalog.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Catalog.API.Protos.Permission;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpcClient<PermissionClient>(o => {
    o.Address = new Uri(builder.Configuration["GRPC_Permission_Server_Address"]);
});
builder.Services.AddLogging(logging =>
logging.AddSeq(builder.Configuration.GetSection("Seq")));

builder.Services.AddInfrastructureSetup();
builder.Services.AddDataBaseSetup(builder.Configuration);

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
