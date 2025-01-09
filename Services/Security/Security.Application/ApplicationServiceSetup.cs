using Microsoft.Extensions.DependencyInjection;
using Security.Application.Contracts.Interface;
using Security.Application.Service.Notification;

namespace Security.Application;

public static class ApplicationServiceSetup
{
    public static void AddApplicationServiceSetup(this IServiceCollection services)
    {
        services.AddTransient<IMessageBrokerConnectionService, MessageBrokerConnectionService>();
        services.AddTransient<IMessageBrokerService, MessageBrokerService>();
    }
}
