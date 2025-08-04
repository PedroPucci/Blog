using Blog.Domain.Entity;
using Blog.Infrastracture.Connections;
using Blog.Infrastracture.Repository.Interfaces;

namespace Blog.Infrastracture.Repository
{
    public class PublicationRepository : IPublicationRepository
    {
        private readonly DataContext _context;

        public PublicationRepository(DataContext context)
        {
            _context = context;
        }

        public Task<PublicationEntity> Add(PublicationEntity publicationEntity)
        {
            throw new NotImplementedException();
        }

        public PublicationEntity Delete(PublicationEntity publicationEntity)
        {
            throw new NotImplementedException();
        }

        public Task<List<PublicationEntity>> Get()
        {
            throw new NotImplementedException();
        }

        public PublicationEntity Update(PublicationEntity publicationEntity)
        {
            throw new NotImplementedException();
        }
    }
}