using Application.Dto;
using Domain.Abstractions;
using MediatR;

namespace Application.UserCases.Users.Commands.Enabled
{
    public class EnabledUserHandler : IRequestHandler<EnabledUserCommand, MessageDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public EnabledUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<MessageDto> Handle(EnabledUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetByEmailAsync(command.Email, cancellationToken);
                if (user == null) 
                    return new MessageDto(false, "User not found");

                user.ChangeStatus(command.IsActive);
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
