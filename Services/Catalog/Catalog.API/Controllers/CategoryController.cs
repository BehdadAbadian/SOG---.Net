using Catalog.Application.CategoryCommandQuery.Command;
using Catalog.Application.CategoryCommandQuery.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Catalog.API.Protos.Permission;

namespace Catalog.API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class CategoryController : ControllerBase
{
    private readonly ILogger<CategoryController> _logger;
    private readonly IMediator _mediator;
    private readonly PermissionClient _permission;

    public CategoryController(ILogger<CategoryController> logger, IMediator mediator,PermissionClient permission)
    {
        _logger = logger;
        _mediator = mediator;
        _permission = permission;
    }

    [HttpGet("GetAll")]
    public async Task<List<GetAllCategoryQueryRespond>> GetAll()
    { 

        List<GetAllCategoryQueryRespond> b = new List<GetAllCategoryQueryRespond>();
        var pCheck = await _permission.CheckAsync(new Protos.CheckPermissionRequest { Role = "Admin"});
        if (pCheck.Success)
        {
            _logger.LogInformation("API : Category/GetAll, ip {0}", Request.HttpContext.Connection.RemoteIpAddress);
            return await _mediator.Send(new GetAllCategoryQuery());
        }
        else { return b; }
    }

    [HttpPost("Add")]
    public async Task<AddCategoryCommandRespond> Add(AddCategoryCommand command)
    {
        _logger.LogInformation("API : Category/Add, ip {0}", Request.HttpContext.Connection.RemoteIpAddress);
        return await _mediator.Send(command);
    }

    [HttpDelete("Delete")]
    public Task<DeleteCategoryCommandRespond> Delete(DeleteCategoryCommand command)
    {
        _logger.LogInformation("API : Category/Delete, ip {0}", Request.HttpContext.Connection.RemoteIpAddress);
        return _mediator.Send(command);
    }

}
