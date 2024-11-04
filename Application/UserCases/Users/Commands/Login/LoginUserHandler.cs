using Application.Dto;
using Domain.Abstractions;
using MediatR;
using Microsoft.AspNet.Identity;

namespace Application.UserCases.Users.Commands.Login
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, MessageDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;

        public LoginUserHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }
        public async Task<MessageDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email, cancellationToken);
                if (user == null)
                {
                    return new MessageDto(false, "User not found");
                }

                var result = _passwordHasher.VerifyHashedPassword(user.Password, request.Password);

                if (result == PasswordVerificationResult.Failed)
                {
                    return new MessageDto(false, "Invalid password");
                }

                return new MessageDto(true, "User logged in successfully");
            }
            catch (Exception ex)
            {
                return new MessageDto(false, $"Error logging in: {ex.Message}");
            }
            

        }
    }
}
