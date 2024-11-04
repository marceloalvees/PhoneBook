using Application.Dto;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.AspNet.Identity;

namespace Application.UserCases.Users.Commands.Save
{
    public class SaveUserHandler : IRequestHandler<SaveUserCommand, MessageDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;

        public SaveUserHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task<MessageDto> Handle(SaveUserCommand command, CancellationToken cancellationToken)
        {

            try
            {
                if (await _unitOfWork.UserRepository.UseryExistAsync(command.Email, cancellationToken))
                {
                    return new MessageDto(false, "User already exists");
                }

                var hash = _passwordHasher.HashPassword(command.Password);
                var user = new User(command.FirstName, command.LastName, command.Gender, command.Email, hash);
                await _unitOfWork.UserRepository.AddAsync(user, cancellationToken);
                await _unitOfWork.CommitAsync();

                return new MessageDto(true, "User created successfully");
            }
            catch (Exception ex)
            {
                return new MessageDto(false, $"Error creating user: {ex.Message}");
            }
        }
    }
}
