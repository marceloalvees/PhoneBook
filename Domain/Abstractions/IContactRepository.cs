using Domain.Entities;

namespace Domain.Abstractions
{
    public interface IContactRepository
    {
        Task<Contact> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Contact>> GetAllAsync(CancellationToken cancellationToken);
        Task AddAsync(Contact contact, CancellationToken cancellationToken);
        Task<List<Contact>> GetByUserIdAsync(int userId, CancellationToken cancellationToken);
        Task<Contact> GetByUserIdAndEmail(int userId, string email, CancellationToken cancellationToken);
    }
}
