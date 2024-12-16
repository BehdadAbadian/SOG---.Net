﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Security.Application.Contracts.Common;
using Security.Application.Login.Command;

namespace Security.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Login")]
        public async Task<SecurityActionResult<List<string>>> Login(LoginCommand command)
        {
            var result = new SecurityActionResult<List<string>>();
            var data = new List<string>();
            var res = await _mediator.Send(command);
            if (res.IsSuccess)
            {
                data.Add("Token : " + res.Token);
                data.Add("User Id : " + res.Id.ToString());
                result.IsSuccess = true;
                result.StatusCode = 200;
                result.Data = data;
                result.Message = res.Message;
               
            }
            else
            {
                result.IsSuccess = false;
                result.StatusCode = 403;
                result.Data = data;
                result.Message = res.Message;
            }
            return result;
        }
    }
}