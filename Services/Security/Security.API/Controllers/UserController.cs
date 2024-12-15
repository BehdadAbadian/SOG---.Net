using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Security.API.Model;
using Security.Infrastructure.Repository;
using Security.Domain.User;
using Security.Application.User.Command;
using Security.Application.User.Query;
using MediatR;
using Security.Application.Contracts.Common;

namespace Security.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("Add")]
        public async Task<SecurityActionResult<SaveUserCommandRespond>> Add(SaveUserCommand command)
        {
            var user = await _mediator.Send(command);
            var result = new SecurityActionResult<SaveUserCommandRespond>();
            result.Data = user;
            result.IsSuccess = true;
            result.StatusCode = 200;
            
            return result;
            

        }

        [HttpGet("GetAll")]
        public async Task<SecurityActionResult<List<GetAllQueryRespond>>> GetAll(GetAllQuery command)
        {
            var users  = await _mediator.Send(command);
            var result = new SecurityActionResult<List<GetAllQueryRespond>>();
            result.IsSuccess = true;
            result.StatusCode = 200;
            result.Data = users;
            result.Page = command.Page;
            result.PageSize = command.PageSize;
            return result;

        }
    }
}
