using Blog.Domain.Entity;
using Blog.Infrastracture.Connections;
using Blog.Infrastracture.Repository.Interfaces;

namespace Blog.Infrastracture.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public Task<UserEntity> Add(UserEntity userEntity)
        {
            throw new NotImplementedException();
        }

        public UserEntity Delete(UserEntity userEntity)
        {
            throw new NotImplementedException();
        }

        public UserEntity Update(UserEntity userEntity)
        {
            throw new NotImplementedException();
        }
    }
}