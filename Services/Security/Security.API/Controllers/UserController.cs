using Microsoft.AspNetCore.Mvc;
using Security.Application.User.Command;
using Security.Application.User.Query;
using MediatR;
using Security.Application.Contracts.Common;
using Microsoft.Extensions.Caching.Memory;

namespace Security.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _cache;

        public UserController(IMediator mediator,IMemoryCache cache)
        {
            _mediator = mediator;
            _cache = cache;
        }


        [HttpPost("Add")]
        public async Task<SecurityActionResult<SaveUserCommandRespond>> Add(SaveUserCommand command)
        {
            var user = await _mediator.Send(command);
            var result = new SecurityActionResult<SaveUserCommandRespond>();
            result.Data = user;
            result.IsSuccess = true;
            result.StatusCode = 200;
            if (result.StatusCode == 200) 
            {
                _cache.Dispose();
            }
            return result;
            

        }

        [HttpGet("GetAll")]
        public async Task<SecurityActionResultWithPaging<List<GetAllQueryRespond>>> GetAll(GetAllQuery command)
        {
            var result = new SecurityActionResultWithPaging<List<GetAllQueryRespond>>();
            var users = new List<GetAllQueryRespond>();
            var cacheKey = $"GetAll-{command.Page.ToString()}-{command.PageSize.ToString()}";
            if (!_cache.TryGetValue(cacheKey, out users))
            {
                users = await _mediator.Send(command);
                var cacheOption = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = null,
                    SlidingExpiration = null,
                };
                _cache.Set(cacheKey, users, cacheOption);
            }
            result.IsSuccess = true;
            result.StatusCode = 200;
            result.Data = users;
            result.Page = command.Page;
            result.PageSize = command.PageSize;
            return result;

        }
    }
}
