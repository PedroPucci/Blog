using Blog.Domain.Entity;
using Blog.Infrastracture.Connections;
using Blog.Infrastracture.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastracture.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<UserEntity> Add(UserEntity userEntity)
        {
            if (userEntity is null)
                throw new ArgumentNullException(nameof(userEntity), "User cannot be null");

            var result = await _context.UserEntity.AddAsync(userEntity);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public UserEntity Delete(UserEntity userEntity)
        {
            if (userEntity == null)
                throw new ArgumentNullException(nameof(userEntity), "User cannot be null");

            var entity = _context.UserEntity.Find(userEntity.Id);
            if (entity == null)
                throw new KeyNotFoundException($"No User found with ID {userEntity.Id}");

            _context.UserEntity.Remove(userEntity);
            _context.SaveChanges();

            return entity;
        }

        public UserEntity Update(UserEntity userEntity)
        {
            var response = _context.UserEntity.Update(userEntity);
            return response.Entity;
        }

        public async Task<UserEntity?> GetById(int? id)
        {
            return await _context.UserEntity.FirstOrDefaultAsync(userEntity => userEntity.Id == id);
        }
    }
}