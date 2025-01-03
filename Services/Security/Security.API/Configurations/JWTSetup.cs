using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Security.Infrastructure.Utility.Model;
using System.Text;

namespace Security.API.Configurations;

public static class JWTSetup
{
    public static IServiceCollection AddJWT(this IServiceCollection services)
    {
        var sp = services.BuildServiceProvider();
        Configs configs = sp.GetService<IOptions<Configs>>().Value;
        var key = Encoding.UTF8.GetBytes(configs.TokenKey);

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ClockSkew = TimeSpan.FromMinutes(configs.TokenTimeout),
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        return services;

    }
}
