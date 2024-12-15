using MongoDB.Bson.Serialization.Attributes;

namespace Security.API.Model
{
    public class UserDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
