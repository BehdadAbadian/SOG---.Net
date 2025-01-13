using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Notification.Application.Contracts.Interface;
using Notification.Domain.Email;
using Notification.Infrastructure.Pattern;

namespace Notification.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailSender _emailSender;
        private readonly IEmailRepository repository;
        private readonly IUnitOfWork unit;

        public EmailController(IEmailSender emailSender,IEmailRepository repository, IUnitOfWork unit)
        {
            _emailSender = emailSender;
            this.repository = repository;
            this.unit = unit;
        }


        [HttpGet("SendEmail")]
        public async Task<IActionResult> SendEmail() 
        {
            _emailSender.SendEmail("behdad.abadian@gmail.com", "Catalog", "Hi", "Welcome");
            var e = Email.CreateNew("as", "dvvd", "dcd", "scs",5);
            await repository.CreateAsync(e);
            await unit.SaveChangesAsync();
            return Accepted();
        }
    }
}
