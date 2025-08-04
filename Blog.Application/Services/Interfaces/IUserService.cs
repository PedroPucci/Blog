using Blog.Application.ExtensionError;
using Blog.Domain.Dto;
using Blog.Domain.Entity;

namespace Blog.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<Result<UserEntity>> Add(UserEntity userEntity);
        Task<Result<UserEntity>> Update(UserDto userCreateUpdateDto);
    }
}