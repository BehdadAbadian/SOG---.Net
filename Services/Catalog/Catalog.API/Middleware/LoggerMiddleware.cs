
using System.Runtime.CompilerServices;

namespace Catalog.API.Middleware;

public class LoggerMiddleware : IMiddleware
{
    private readonly ILogger<LoggerMiddleware> _logger;

    public LoggerMiddleware(ILogger<LoggerMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {

        var path =  context.Request.Path;
        var method = context.Request.Method;
        var body = context.Request.Body;
        var header = context.Request.Headers; 
        var query = context.Request.Query;
        var cookies = context.Request.Cookies;

        var rHttpContext =  context.Response.HttpContext;
        var rStatusCode = context.Response.StatusCode;
        var rCookies = context.Response.Cookies;
        var rbody = context.Response.Body;
        var rheader = context.Response.Headers; 
        var rquery = context.Response; 
       
        var User = context.User; 
         


        await next.Invoke(context);

    }

}
