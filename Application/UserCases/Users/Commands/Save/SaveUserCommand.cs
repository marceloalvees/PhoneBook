using Domain.Entities;
using MediatR;

namespace Application.UserCases.Users.Commands.Save
{
    public class SaveUserCommand : IRequest<User>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Gender { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public bool IsActive { get; set; }
    }
}
