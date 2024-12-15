using Microsoft.Extensions.DependencyInjection;
using Security.Domain.User;
using Security.Infrastructure.Pattern;
using Security.Infrastructure.Repository;

namespace Security.Infrastructure;

public static class InfrastructureSetup
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();

    }
}
