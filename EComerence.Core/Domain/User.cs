using System;
using System.Collections.Generic;
using System.Text;

namespace EComerence.Core.Domain
{
    public class User : Entity
    {
        public string Name { get; protected set; }

        public string Role { get; protected set; }

        public string Email { get; protected set; }

        public string Password { get; protected set; }

        public string City { get; protected set; }

        public string Address { get; protected set; }
        
        public string PostalCode { get; protected set; }

        public DateTime CreatedAt { get; protected set; }

        protected User()
        {

        }
        public User(Guid id, string name, string email, string password, string role, string city, string address, string postalCode )
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            CreatedAt = DateTime.UtcNow;
            Role = role;
            City = city;
            PostalCode = postalCode;
            Address = address;
        }


    }
}
