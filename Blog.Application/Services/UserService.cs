using Blog.Application.ExtensionError;
using Blog.Application.Services.Interfaces;
using Blog.Domain.Dto;
using Blog.Domain.Entity;
using Blog.Infrastracture.RepositoryUoW;

namespace Blog.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryUoW _repositoryUoW;

        public UserService(IRepositoryUoW repositoryUoW)
        {
            _repositoryUoW = repositoryUoW;
        }

        public Task<Result<UserEntity>> Add(UserEntity userEntity)
        {
            throw new NotImplementedException();
        }

        public Task<Result<UserEntity>> Update(UserDto userCreateUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
 }