using Catalog.Domain.Categories;
using Catalog.Domain.Products;
using Catalog.Infrastructure.Patterns;
using Catalog.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure;

public static class InfrastructureSetup
{
    public static void AddInfrastructureSetup(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
    }
}
