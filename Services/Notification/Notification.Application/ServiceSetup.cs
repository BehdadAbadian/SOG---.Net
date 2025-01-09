using Microsoft.Extensions.DependencyInjection;
using Notification.Application.Contracts.Interface;
using Notification.Application.Email.Service;

namespace Notification.Application;

public static class ServiceSetup
{
    public static void AddServiceSetup(this IServiceCollection services)
    {
        services.AddTransient<IEmailSender, EmailSenderService>();
    }
}
