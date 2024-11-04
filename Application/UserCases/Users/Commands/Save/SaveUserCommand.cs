using Application.Dto;
using Domain.Entities;
using MediatR;

namespace Application.UserCases.Users.Commands.Save
{
    public class SaveUserCommand : UserDto, IRequest<MessageDto>
    {
        public required string Password { get; set; }
    }
}
