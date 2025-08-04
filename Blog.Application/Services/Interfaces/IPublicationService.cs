using Blog.Application.ExtensionError;
using Blog.Domain.Entity;

namespace Blog.Application.Services.Interfaces
{
    public interface IPublicationService
    {
        Task<Result<PublicationEntity>> Add(PublicationEntity publicationEntity);
        Task<Result<PublicationEntity>> Update(PublicationEntity publicationCreateUpdateDto);
        Task Delete(int publicationId);
        Task<List<PublicationEntity>> Get();
    }
}