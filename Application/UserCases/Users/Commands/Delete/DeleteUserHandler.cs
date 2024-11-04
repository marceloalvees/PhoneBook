using Application.Dto;
using Domain.Abstractions;
using MediatR;

namespace Application.UserCases.Users.Commands.Delete
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, MessageDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<MessageDto> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetByEmailAsync(request.email, cancellationToken);

                if(user is null)
                    return new MessageDto(false, "User not found");

                _unitOfWork.UserRepository.Delete(user);
                await _unitOfWork.CommitAsync();

                return new MessageDto(true, "User deleted successfully");
            }
            catch (Exception ex)
            {
                return new MessageDto(false, $"Error deleting user: {ex.Message}");
            }
        }
    }
}
