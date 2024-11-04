using Application.Dto;
using MediatR;

namespace Application.UserCases.Users.Commands.Enabled
{
    public class EnabledUserCommand : IRequest<MessageDto>
    {
        public required string Email { get; set; }
        public bool IsActive { get; set; }
    }
}
