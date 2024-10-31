using Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public sealed class User : Entity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set;}
        public string Gender { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public bool IsActive { get; private set; }

        public User()
        {
        }
        public User(string firstName, string lastName, string gender, string email, string password)
        {
            Validate(firstName, lastName,gender, email);
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Email = email;
            Password = password;
            IsActive = true;
        }

        public void Update(string firstName, string lastName, string email, string gender)
        {
            Validate(firstName, lastName, gender, email);
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Email = email;
        }

        public void Validate(string firstName, string lastName, string gender, string email)
        {
            DomainValidation.When(string.IsNullOrEmpty(firstName), "First name is required");
            DomainValidation.When(firstName.Length < 3, "Invalid First name, too short, minimum 3 characters");
            DomainValidation.When(string.IsNullOrEmpty(lastName), "Last name is required");
            DomainValidation.When(lastName.Length < 3, "Invalid Last name, too short, minimum 3 characters");
            DomainValidation.When(email.Length < 6, "Invalid Email, too short, minimum 6 characters");
            DomainValidation.When(!email.Contains("@"), "Invalid Email, must contain @");
            DomainValidation.When(string.IsNullOrEmpty(gender), "Gender is required");
            DomainValidation.When(string.IsNullOrEmpty(Password), "Password is required");
        }
    }
}
