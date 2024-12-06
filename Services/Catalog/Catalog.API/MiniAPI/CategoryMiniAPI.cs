using Catalog.Application.CategoryCommandQuery.Command;
using Catalog.Application.CategoryCommandQuery.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using static Catalog.API.Protos.Permission;

namespace Catalog.API.MiniAPI;

public static class CategoryMiniAPI
{
    public static void AddMiniAPI(this WebApplication app)
    {
        app.MapGet("/Category/getall", async (IMediator _mediator, PermissionClient _permission) =>
        {
            List<GetAllCategoryQueryRespond> b = new List<GetAllCategoryQueryRespond>();
            var pCheck = await _permission.CheckAsync(new Catalog.API.Protos.CheckPermissionRequest { Role = "Admin" });
            if (pCheck.Success)
            {
                return await _mediator.Send(new GetAllCategoryQuery());
            }
            else { return b; }
        });

        app.MapPost("/Category/add", async (IMediator _mediator, AddCategoryCommand command) =>
        {
            return await _mediator.Send(command);
        });

        app.MapDelete("/category/delete", async (IMediator _mediator, [FromBody] DeleteCategoryCommand command) =>
        {
            return await _mediator.Send(command);
        });
    }
}
