using Blog.Domain.Entity;
using Blog.Infrastracture.Connections;
using Blog.Infrastracture.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastracture.Repository
{
    public class PublicationRepository : IPublicationRepository
    {
        private readonly DataContext _context;

        public PublicationRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<PublicationEntity> Add(PublicationEntity publicationEntity)
        {
            if (publicationEntity is null)
                throw new ArgumentNullException(nameof(publicationEntity), "Publication cannot be null");

            var result = await _context.PublicationEntity.AddAsync(publicationEntity);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public PublicationEntity Delete(PublicationEntity publicationEntity)
        {
            if (publicationEntity == null)
                throw new ArgumentNullException(nameof(publicationEntity), "Publication cannot be null");

            var entity = _context.PublicationEntity.Find(publicationEntity.Id);
            if (entity == null)
                throw new KeyNotFoundException($"No publication found with ID {publicationEntity.Id}");

            _context.PublicationEntity.Remove(entity);
            _context.SaveChanges();

            return entity;
        }

        public async Task<List<PublicationEntity>> Get()
        {
            return await _context.PublicationEntity
                .AsNoTracking()
                .OrderBy(publication => publication.Id)
                .Select(publication => new PublicationEntity
                {
                    Id = publication.Id,
                    UserId = publication.Id,
                    Content = publication.Content,
                    Title = publication.Title,
                    CreateDate = publication.CreateDate,
                })
            .ToListAsync();
        }

        public PublicationEntity Update(PublicationEntity publicationEntity)
        {
            var response = _context.PublicationEntity.Update(publicationEntity);
            return response.Entity;
        }
    }
}