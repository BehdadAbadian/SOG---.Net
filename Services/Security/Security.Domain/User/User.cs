﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Security.Domain.User
{
    public class User
    {
        public Guid Id { get; private set; }
        public string? Name { get; private set; }
        public string? Email { get; private set; }
        public string? Password { get; private set; }
        public string? PasswordSalt { get; set; }
        public DateTime CreationDate { get; private set; }
        public DateTime LastLogin { get; private set; }

        public static User CreateNew(string name, string email, string password, string passwordSalt)
        {
            var id = Guid.NewGuid();
            return new User(id, name, email, password, passwordSalt);

        }

        private User() { }
        private User(Guid id, string name, string email, string password, string passwordSalt)
        {


            Id = id;
            Name = name;
            Email = email;
            Password = password;
            PasswordSalt = passwordSalt;
            CreationDate = DateTime.Now;
            LastLogin = DateTime.Now;
        }
    }
    
}
