
namespace Domain.Abstractions
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IContactRepository ContactRepository { get; }
        Task CommitAsync();
    }
}
