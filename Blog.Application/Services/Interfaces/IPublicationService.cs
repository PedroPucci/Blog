using Blog.Application.ExtensionError;
using Blog.Domain.Dto;
using Blog.Domain.Entity;

namespace Blog.Application.Services.Interfaces
{
    public interface IPublicationService
    {
        Task<Result<PublicationEntity>> Add(PublicationEntity publicationEntity);
        Task<Result<PublicationEntity>> Update(PublicationDto publicationCreateUpdateDto);
        Task Delete(int publicationId);
        Task<List<PublicationEntity>> Get();
    }
}