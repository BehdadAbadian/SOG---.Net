using Catalog.Application.CategoryCommandQuery.Command;
using Serilog;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace Catalog.API.Middleware;

public class DataValidationMiddleware : IMiddleware
{


    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (context.Request.Path == "/category/add")
        {
            context.Request.EnableBuffering();

            using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
            {

                var body = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;
                try
                {
                    var addCategoryCommand = JsonSerializer.Deserialize<AddCategoryCommand>(body);

                    if (string.IsNullOrEmpty(addCategoryCommand.Title))
                    {
                        context.Response.StatusCode = 400;
                        Log.Information("In Input data Title is null, status code : {0}", context.Response.StatusCode);
                        await context.Response.WriteAsJsonAsync<AddCategoryCommandRespond>(new AddCategoryCommandRespond { Messasge = "Title is null" });

                    }
                    else
                    {
                        await next.Invoke(context);
                    }
                }
                catch (JsonException je)
                {
                    context.Response.StatusCode = 400;
                    Log.Warning("Invalid Input, Exception message : {0}, status code : {1}", je.Message, context.Response.StatusCode);
                    await context.Response.WriteAsJsonAsync<AddCategoryCommandRespond>(new AddCategoryCommandRespond { Messasge = "Invalid Input" });

                };
            }
        }
        else
        {
            await next.Invoke(context);
        }

    }
}
