using Microsoft.AspNetCore.Mvc;
using Security.Application.User.Command;
using Security.Application.User.Query;
using MediatR;
using Security.Application.Contracts.Common;
using Microsoft.Extensions.Caching.Memory;
using Security.Application.Contracts.Interface;
using Security.Application.Service.Notification;
using Security.API.Model;

namespace Security.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _cache;
        private readonly IPermissionApplicationService _permissionApplication;
        private readonly IMessageBrokerService _messageBroker;

        public UserController(IMediator mediator,IMemoryCache cache, IPermissionApplicationService permissionApplication, IMessageBrokerService messageBroker)
        {
            _mediator = mediator;
            _cache = cache;
            _permissionApplication = permissionApplication;
            _messageBroker = messageBroker;
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

        [HttpGet("Count")]
        public async Task<IActionResult> SendMessage(int count)
        {
            for (int i = 1; i < count; i++)
            {
                var order = new EmailDto
                {
                    Sender = "User Management",
                    EmailAddress = "behdad.abadian@gmail.com",
                    Subject = "Welcome" + i.ToString(),
                    Body = DateTime.Now.AddDays(-i).ToShortDateString(),
                    
                };
                string queueName = "email-queue";
                await _messageBroker.SendMessage(queueName, order);

            }
            return Ok();
        }
    }
}
