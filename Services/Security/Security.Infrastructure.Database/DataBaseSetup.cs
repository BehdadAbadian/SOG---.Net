using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Security.Infrastructure.Database;

public static class DataBaseSetup
{
    public static void AddDataBaseSetup(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SOG-Security-API");
        services.AddDbContext<SecurityContext>(options =>
        {
            options.UseSqlServer(connectionString,
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure();
                });
        });
    }
}
