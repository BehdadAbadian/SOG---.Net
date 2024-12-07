using Catalog.Application.CategoryCommandQuery.Command;
using Catalog.Application.CategoryCommandQuery.Query;
using Catalog.Appllication.Contract.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using static Catalog.API.Protos.Permission;

namespace Catalog.API.MiniAPI;

public static class CategoryMiniAPI
{
    public static void AddMiniAPI(this WebApplication app)
    {
        app.MapGet("/Category/getall", async Task<CatalogActionResult<List<GetAllCategoryQueryRespond>>> (IMediator _mediator, PermissionClient _permission) =>
        {
        
            var pCheck = await _permission.CheckAsync(new Catalog.API.Protos.CheckPermissionRequest { Role = "Admin" });
            if (pCheck.Success)
            {
                var data = await _mediator.Send(new GetAllCategoryQuery());
                return new CatalogActionResult<List<GetAllCategoryQueryRespond>>
                {
                    Data = data,
                    IsSuccess = true,
                    StatusCode = 200,
                    Message = "Ok"
                   
                };
            }
            else 
            { 
                return new CatalogActionResult<List<GetAllCategoryQueryRespond>> 
                {
                    
                    IsSuccess = false,
                    StatusCode = 403,
                    Message = "access to the requested resource is forbidden"
                }; 
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
