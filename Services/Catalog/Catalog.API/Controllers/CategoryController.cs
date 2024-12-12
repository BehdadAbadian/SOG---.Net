﻿using Catalog.Application.CategoryCommandQuery.Command;
using Catalog.Application.CategoryCommandQuery.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using static Catalog.API.Protos.Permission;

namespace Catalog.API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class CategoryController : ControllerBase
{
    private readonly ILogger<CategoryController> _logger;
    private readonly IMediator _mediator;
    private readonly PermissionClient _permission;
    private readonly IMemoryCache _memoryCache;


    public CategoryController(ILogger<CategoryController> logger, IMediator mediator, PermissionClient permission, IMemoryCache memoryCache)
    {
        _logger = logger;
        _mediator = mediator;
        _permission = permission;
        _memoryCache = memoryCache;
    }

    [HttpGet("GetAll")]
    public async Task<List<GetAllCategoryQueryRespond>> GetAll()
    {

        List<GetAllCategoryQueryRespond> result = new List<GetAllCategoryQueryRespond>();
        var pCheck = await _permission.CheckAsync(new Protos.CheckPermissionRequest { Role = "Admin" });
        if (pCheck.Success)
        {
            _logger.LogInformation("API : Category/GetAll, ip {0}", Request.HttpContext.Connection.RemoteIpAddress);
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
        else { return result; }
    }

    [HttpPost("Add")]
    public async Task<AddCategoryCommandRespond> Add(AddCategoryCommand command)
    {
        _logger.LogInformation("API : Category/Add, ip {0}", Request.HttpContext.Connection.RemoteIpAddress);
        var results = await _mediator.Send(command);
        if (results.Messasge != "نام تکراری میباشد!")
            _memoryCache.Remove("GetAll");

        return results;

    }

    [HttpDelete("Delete")]
    public Task<DeleteCategoryCommandRespond> Delete(DeleteCategoryCommand command)
    {
        _logger.LogInformation("API : Category/Delete, ip {0}", Request.HttpContext.Connection.RemoteIpAddress);
        return _mediator.Send(command);
    }

}
