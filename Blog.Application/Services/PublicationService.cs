using Blog.Application.ExtensionError;
using Blog.Application.Services.Interfaces;
using Blog.Domain.Entity;
using Blog.Infrastracture.RepositoryUoW;

namespace Blog.Application.Services
{
    public class PublicationService : IPublicationService
    {
        private readonly IRepositoryUoW _repositoryUoW;

        public PublicationService(IRepositoryUoW repositoryUoW)
        {
            _repositoryUoW = repositoryUoW;
        }

        public Task<Result<PublicationEntity>> Add(PublicationEntity publicationEntity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<PublicationEntity>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<Result<PublicationEntity>> Update(PublicationEntity publicationCreateUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}