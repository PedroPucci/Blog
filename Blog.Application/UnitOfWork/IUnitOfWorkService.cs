using Blog.Application.Services;

namespace Blog.Application.UnitOfWork
{
    public interface IUnitOfWorkService
    {
        UserService UserService { get; }
        PublicationService PublicationService { get; }
    }
}