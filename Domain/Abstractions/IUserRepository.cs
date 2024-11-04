using Domain.Entities;

namespace Domain.Abstractions
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken);
        Task AddAsync(User user, CancellationToken cancellationToken);
        void Update(User user);
        void Delete(User user);
        Task<bool> UseryExistAsync(string email, CancellationToken cancellationToken);
    }
}
