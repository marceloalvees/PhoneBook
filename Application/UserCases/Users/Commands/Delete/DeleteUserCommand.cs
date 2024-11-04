using Application.Dto;
using MediatR;

namespace Application.UserCases.Users.Commands.Delete
{
    public record class DeleteUserCommand(string email) : IRequest<MessageDto>;
}
