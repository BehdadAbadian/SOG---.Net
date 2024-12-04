using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using User.API.Models;

namespace User.API.Properties;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{



    [HttpGet("GetAll")]
    public List<UsersData> GetAll(int count = 100)
    {
        List<UsersData> users = new List<UsersData>();
        Random random = new Random();
        for (int i = 0; i < count; i++) 
        {
            var user = new UsersData { 
                Id = Guid.NewGuid(),
                Age = random.Next(15,65),
                Name = GenerateRandomString(7),
            };
            users.Add(user);
        }
        return users;
}

    private string GenerateRandomString(int v)
    {
        const string Chars = "ABCDEFGHIJKLMNPQWVXZRY123456789";
        StringBuilder sb = new StringBuilder(v);
        Random random = new Random();
        for (int i = 0; i < v; i++) 
        {
            sb.Append(Chars[random.Next(Chars.Length)]);
        }
        return sb.ToString();
    }
}