using Catalog.Application.CategoryCommandQuery.Command;
using Catalog.Application.CategoryCommandQuery.Query;
using Catalog.Appllication.Contract.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using static Catalog.API.Protos.Permission;

namespace Catalog.API.MiniAPI;

public static class CategoryMiniAPI
{
    public static void AddMiniAPI(this WebApplication app)
    {
        app.MapGet("/Category/getall", async Task<CatalogActionResult<List<GetAllCategoryQueryRespond>>> (IMediator _mediator, PermissionClient _permission) =>
        {
            var result = new CatalogActionResult<List<GetAllCategoryQueryRespond>>();
            var pCheck = await _permission.CheckAsync(new Catalog.API.Protos.CheckPermissionRequest { Role = "Admin" });

            if (pCheck.Success)
            {           
                    var data = await _mediator.Send(new GetAllCategoryQuery());

                    result.Data = data;
                    result.IsSuccess = true;
                    result.StatusCode = 200;
                    result.Message = "OK";
                    return result;                
            }
            else 
            {
                result.IsSuccess = false;
                result.StatusCode = 403;
                result.Message = "access to the requested resource is forbidden"; 
                return result;
                 
            }
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
