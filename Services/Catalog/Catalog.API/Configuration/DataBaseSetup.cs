using Catalog.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Configuration
{
    public static class DataBaseSetup
    {
        public static void AddDataBaseSetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var connectionString = configuration.GetConnectionString("SOG-Catalog-API");
            services.AddDbContext<CatalogContext>(options =>
            {
                options.UseSqlServer(connectionString,
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure();
                    });
            });
        }
    }
}
