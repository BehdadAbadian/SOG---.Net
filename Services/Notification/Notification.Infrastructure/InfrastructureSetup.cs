using Microsoft.Extensions.DependencyInjection;
using Notification.Domain.Email;
using Notification.Infrastructure.Pattern;
using Notification.Infrastructure.Repository;

namespace Notification.Infrastructure;

public static class InfrastructureSetup
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IEmailRepository, EmailRepository>();



    }
}