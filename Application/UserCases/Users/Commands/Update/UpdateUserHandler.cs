using Application.Dto;
using Domain.Abstractions;
using MediatR;
using Microsoft.AspNet.Identity;

namespace Application.UserCases.Users.Commands.Update
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, MessageDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MessageDto> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
                if (!await _unitOfWork.UserRepository.UseryExistAsync(command.Email, cancellationToken))
                {
                    return new MessageDto(false, "User not found");
                }
                var user = await _unitOfWork.UserRepository.GetByEmailAsync(command.Email, cancellationToken);
                user.Update(command.FirstName, command.LastName, command.Email, command.Gender);
                _unitOfWork.UserRepository.Update(user);
                await _unitOfWork.CommitAsync();

                return new MessageDto(true, "User updated successfully");
            }
            catch(Exception ex)
            {
                return new MessageDto(false, $"Error updating user: {ex.Message}");
            }
            

        }
    }
}
