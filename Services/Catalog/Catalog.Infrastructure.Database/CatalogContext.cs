using Catalog.Domain.Categories;
using Catalog.Domain.Products;
using Catalog.Infrastructure.Database.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Database;

public class CatalogContext : DbContext
{
    public DbSet<Category> categories { get; set; }
    public DbSet<Product> products { get; set; }

    public CatalogContext(DbContextOptions<CatalogContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = typeof(CategoryConfiguration).Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        base.OnModelCreating(modelBuilder);
    }
}
