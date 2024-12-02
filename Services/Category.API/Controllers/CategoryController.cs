using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ILogger<CategoryController> _logger;

    public CategoryController(ILogger<CategoryController> logger)
    {
        _logger = logger;
    }

    [HttpGet("Test")]
    public async Task<string> Get() 
    {
        _logger.LogInformation("API : Category/Get, ip {0}, User Value : ", Request.HttpContext.Connection.RemoteIpAddress);
        var message = "OK";
        return message;
    }
}
