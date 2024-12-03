using Catalog.API.Configuration;
using Catalog.Application.CategoryCommandQuery.Command;
using Catalog.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLogging(logging =>
logging.AddSeq(builder.Configuration.GetSection("Seq")));

builder.Services.AddInfrastructureSetup();
builder.Services.AddDataBaseSetup(builder.Configuration);

builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssemblies(typeof(AddCategoryCommand).Assembly);
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
