using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Security.API.Model;
using Security.Infrastructure.Repository;
using Security.Domain.User;

namespace Security.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _repository;

        public UserController(UserRepository repository)
        {
            _repository = repository;
        }


        [HttpPost("Add")]
        public async Task Add(UserDto command)
        {
            var user = Security.Domain.User.User.CreateNew(command.Name,command.Email,command.Password);
            await _repository.Add(user);

        }

        [HttpGet("GetAll")]
        public async Task<List<User>> GetAll()
        {
            return await _repository.GetAll();
        }
    }
}
