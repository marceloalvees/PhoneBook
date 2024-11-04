using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ContactRepository : IContactRepository
    {
        protected readonly AppDbContext _context;

        public ContactRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Contact contact, CancellationToken cancellationToken)
        {
            if (contact == null)
            {
                throw new ArgumentNullException(nameof(contact));
            }
            await _context.Contacts.AddAsync(contact, cancellationToken);
        }
        
        public async Task<IEnumerable<Contact>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Contacts.ToListAsync(cancellationToken);
        }

        public async Task<Contact> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }
            return await _context.Contacts.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<List<Contact>> GetByUserIdAsync (int userId, CancellationToken cancellationToken)
        {
            if (userId <= 0)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            return await _context.Contacts.Where(x => x.UserId == userId).ToListAsync(cancellationToken);
        }

        public async Task<Contact> GetByUserIdAndEmail(int userId, string email, CancellationToken cancellationToken)
        {
            if (userId <= 0)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email));
            }
            return await _context.Contacts.FirstOrDefaultAsync(x => x.UserId == userId && x.Email == email, cancellationToken);
        }
    }
}
