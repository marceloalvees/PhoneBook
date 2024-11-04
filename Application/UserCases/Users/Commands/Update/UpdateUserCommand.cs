using Application.Dto;
using MediatR;

namespace Application.UserCases.Users.Commands.Update
{
    public class UpdateUserCommand : UserDto, IRequest<MessageDto>
    {  
    }
}
