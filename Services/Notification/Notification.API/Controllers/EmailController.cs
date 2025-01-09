using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Notification.Application.Contracts.Interface;

namespace Notification.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailSender _emailSender;

        public EmailController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }


        [HttpGet("SendEmail")]
        public IActionResult SendEmail() 
        {
            _emailSender.SendEmail("behdad.abadian@gmail.com", "Catalog", "Hi", "Welcome");
            return Accepted();
        }
    }
}
