using System;
using System.Collections.Generic;
using System.Text;

namespace EComerence.Core.Domain
{
    public class User : Entity
    {
        private static List<string> _roles = new List<string>
        {
            "user", "admin"
        };

        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Role { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string City { get; protected set; }
        public string Address { get; protected set; }      
        public string PostalCode { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
      public virtual List<UserProductProbability> UserProductProbabilities { get; set; }


      protected User()
        {

        }
        public User(Guid id, string firstName, string lastName , string email, string password, string role, string city, string address, string postalCode )
        {
            Id = id;
            SetRole(role);
            SetFirstName(firstName);
            SetLastName(lastName);
            SetEmail(email);
            SetPassword(password);
            CreatedAt = DateTime.UtcNow;
            City = city;
            PostalCode = postalCode;
            Address = address;
        }
        public void SetFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new Exception($"User can not have an empty first name.");
            }
            FirstName = firstName;
        }
        public void SetLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new Exception($"User can not have an empty last name.");
            }
            LastName = lastName;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception($"User can not have an empty email.");
            }
            Email = email;
        }

        public void SetRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                throw new Exception($"User can not have an empty role.");
            }
            role = role.ToLowerInvariant();
            if (!_roles.Contains(role))
            {
                throw new Exception($"User can not have a role: '{role}'.");
            }
            Role = role;
        }

        public void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception($"User can not have an empty password.");
            }
            Password = password;
        }


    }
}
