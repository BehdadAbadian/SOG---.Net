using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Notification.Infrastructure.Database;

public static class DataBaseSetup
{
    public static void AddDataBaseSetup(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SOG-Notification-API");
        services.AddDbContext<NotificationContext>(options =>
        {
            options.UseSqlServer(connectionString,
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure();
                });
        });
    }
    public static IApplicationBuilder AddDataBaseScope(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var notificationContext = scope.ServiceProvider.GetRequiredService<NotificationContext>();
            notificationContext.Database.Migrate();
        }
        return app;
    }
}
