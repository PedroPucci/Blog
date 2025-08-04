using Blog.Domain.Entity;

namespace Blog.Infrastracture.Repository.Interfaces
{
    public interface IPublicationRepository
    {
        Task<PublicationEntity> Add(PublicationEntity publicationEntity);
        PublicationEntity Update(PublicationEntity publicationEntity);
        PublicationEntity Delete(PublicationEntity publicationEntity);
        Task<List<PublicationEntity>> Get();
        Task<PublicationEntity?> GetById(int? id);
    }
}