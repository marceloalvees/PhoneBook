using Application.Dto;
using MediatR;

namespace Application.UserCases.Users.Commands.Login
{
    public class LoginUserCommand : IRequest<MessageDto>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
