using Domain.Validation;
using System.Reflection;

namespace Domain.Entities
{
    public class Contact : Entity
    {
        public int UserId { get; init; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }

        public Contact(int userId, string name, string email, string phone)
        {
            Validate(name, email, phone);
            UserId = userId;
            Name = name;
            Email = email;
            Phone = phone;
        }

        public void Update(string name, string email, string phone)
        {
            Validate(name, email, phone);
            Name = name;
            Email = email;
            Phone = phone;
        }

        public void Validate( string name, string email, string phone)
        {
            DomainValidation.When(string.IsNullOrEmpty(name), "Name is required");
            DomainValidation.When(name.Length < 3, "Invalid name, too short, minimum 3 characters");
            DomainValidation.When(email.Length < 6, "Invalid Email, too short, minimum 6 characters");
            DomainValidation.When(!email.Contains("@"), "Invalid Email, must contain @");
            DomainValidation.When(string.IsNullOrEmpty(phone), "Phone is required");
        }
    }
}
