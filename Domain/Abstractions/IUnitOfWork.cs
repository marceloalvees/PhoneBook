﻿
namespace Domain.Abstractions
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        Task CommitAsync();
    }
}
