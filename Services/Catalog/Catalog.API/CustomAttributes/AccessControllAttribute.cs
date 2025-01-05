using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static Catalog.API.Protos.Permission;

namespace Catalog.API.CustomAttributes;

public class AccessControllAttribute : ActionFilterAttribute
{
    public string Permission { get; set; }
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        string? userId = GetUserGuidFromToken(context);
        if (userId != null) 
        {
            var _PermissionService = context.HttpContext.RequestServices.GetService<PermissionClient>();
            var pCheck = await _PermissionService.CheckAsync(new Protos.CheckPermissionRequest { Userid = userId, Permissionname = Permission });
            if (!pCheck.Success)
                context.Result = new BadRequestObjectResult("Not Access");
            await base.OnActionExecutionAsync(context, next);
        }
        context.Result = new BadRequestObjectResult("Not Access");
    }

    private static string? GetUserGuidFromToken(ActionExecutingContext context)
    {
        if (context.HttpContext.Request.Headers.TryGetValue("Authorization", out var headerAuth))
        {
            var jwtToken = headerAuth.First().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1];
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadToken(jwtToken) as JwtSecurityToken;
            var payloads = token?.Payload;
            var userId = payloads?.Claims?.FirstOrDefault(claims => claims.Type == "UserGuid")?.Value;
            return userId;
        }
        return null;
 
    }
}
