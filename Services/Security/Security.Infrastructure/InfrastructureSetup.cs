using Microsoft.Extensions.DependencyInjection;
using Security.Domain.Security;
using Security.Domain.User;
using Security.Infrastructure.Pattern;
using Security.Infrastructure.Repository;

namespace Security.Infrastructure;

public static class InfrastructureSetup
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<PermissionRepository>();
        services.AddTransient<RolePermissionRepository>();
        services.AddTransient<RoleUserRepository>();


    }
}
