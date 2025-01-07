using Catalog.API.CustomAttributes;
using Catalog.Application.CategoryCommandQuery.Command;
using Catalog.Application.CategoryCommandQuery.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Serilog;
using static Catalog.API.Protos.Permission;

namespace Catalog.API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly PermissionClient _permission;
    private readonly IMemoryCache _memoryCache;


    public CategoryController(IMediator mediator, PermissionClient permission, IMemoryCache memoryCache)
    {
        _mediator = mediator;
        _permission = permission;
        _memoryCache = memoryCache;
    }

    [HttpGet("GetAll")]
    [AccessControll(Permission = "Product-GetAll")]
    public async Task<List<GetAllCategoryQueryRespond>> GetAll()
    {

        List<GetAllCategoryQueryRespond> result = new List<GetAllCategoryQueryRespond>();

            Log.Information("API : Category/GetAll, ip {0}", Request.HttpContext.Connection.RemoteIpAddress);
            var cacheKey = "GetAll";
            if (!_memoryCache.TryGetValue(cacheKey, out result))
            {
                result = await _mediator.Send(new GetAllCategoryQuery());

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = null,
                    SlidingExpiration = null
                };
                _memoryCache.Set(cacheKey, result, cacheEntryOptions);
            }

            return result;

    }

    [HttpPost("Add")]
    public async Task<AddCategoryCommandRespond> Add(AddCategoryCommand command)
    {
        Log.Information("API : Category/Add, ip {0}", Request.HttpContext.Connection.RemoteIpAddress);
        var results = await _mediator.Send(command);
        if (results.Messasge != "نام تکراری میباشد!")
            _memoryCache.Remove("GetAll");

        return results;

    }

    [HttpDelete("Delete")]
    public Task<DeleteCategoryCommandRespond> Delete(DeleteCategoryCommand command)
    {
        Log.Information("API : Category/Delete, ip {0}", Request.HttpContext.Connection.RemoteIpAddress);
        return _mediator.Send(command);
    }

}
