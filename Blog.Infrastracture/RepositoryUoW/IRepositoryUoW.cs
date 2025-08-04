using Blog.Infrastracture.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Blog.Infrastracture.RepositoryUoW
{
    public interface IRepositoryUoW
    {
        IUserRepository UserRepository { get; }
        IPublicationRepository PublicationRepository { get; }

        Task SaveAsync();
        void Commit();
        IDbContextTransaction BeginTransaction();
    }
}