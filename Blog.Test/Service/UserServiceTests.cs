using Blog.Application.Services;
using Blog.Domain.Dto;
using Blog.Domain.Entity;
using Blog.Infrastracture.Repository.Interfaces;
using Blog.Infrastracture.RepositoryUoW;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;

namespace Blog.Test.Service
{
    public class UserServiceTests
    {
        private readonly Mock<IRepositoryUoW> _repositoryUoWMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _repositoryUoWMock = new Mock<IRepositoryUoW>();
            _userRepositoryMock = new Mock<IUserRepository>();

            _repositoryUoWMock.Setup(x => x.UserRepository).Returns(_userRepositoryMock.Object);
            _repositoryUoWMock.Setup(x => x.BeginTransaction()).Returns(Mock.Of<IDbContextTransaction>());

            _userService = new UserService(_repositoryUoWMock.Object);
        }

        [Fact]
        public async Task Add_ShouldReturnError_WhenValidationFails()
        {
            var user = new UserEntity();

            var result = await _userService.Add(user);

            Assert.False(result.Success);
            Assert.Contains("Name", result.Message);
        }

        [Fact]
        public async Task Add_ShouldReturnSuccess_WhenValidUser()
        {
            var user = new UserEntity
            {
                Name = "Pedro",
                Email = "pedro@example.com",
                Password = "123456"
            };

            _userRepositoryMock.Setup(x => x.Add(It.IsAny<UserEntity>())).ReturnsAsync(user);
            _repositoryUoWMock.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);

            var result = await _userService.Add(user);

            Assert.True(result.Success);
            _userRepositoryMock.Verify(x => x.Add(It.IsAny<UserEntity>()), Times.Once);
            _repositoryUoWMock.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldReturnError_WhenValidationFails()
        {
            var userDto = new UserDto
            {
                Id = 1,
                Name = "",
                Email = "email-invalido"
            };

            var userEntity = new UserEntity
            {
                Id = userDto.Id,
                Name = "Old Name",
                Email = "old@email.com"
            };

            _userRepositoryMock.Setup(x => x.GetById(userDto.Id)).ReturnsAsync(userEntity);

            var result = await _userService.Update(userDto);

            Assert.False(result.Success);
            Assert.Contains("Name", result.Message);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenUserNotFound()
        {
            var userDto = new UserDto
            {
                Id = 999,
                Name = "Alguém",
                Email = "email@email.com"
            };

            _userRepositoryMock.Setup(x => x.GetById(userDto.Id)).ReturnsAsync((UserEntity?)null);

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => _userService.Update(userDto));

            Assert.Equal("Error updating User", ex.Message);
        }

        [Fact]
        public async Task Update_ShouldReturnSuccess_WhenValidUser()
        {
            var userDto = new UserDto
            {
                Id = 1,
                Name = "Pedro",
                Email = "novo@email.com"
            };

            var existingUser = new UserEntity
            {
                Id = 1,
                Name = "Antigo",
                Email = "antigo@email.com"
            };

            _userRepositoryMock.Setup(x => x.GetById(userDto.Id)).ReturnsAsync(existingUser);
            _repositoryUoWMock.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);

            var result = await _userService.Update(userDto);

            Assert.True(result.Success);
            _userRepositoryMock.Verify(x => x.Update(It.Is<UserEntity>(
                u => u.Id == userDto.Id &&
                     u.Name == userDto.Name &&
                     u.Email == userDto.Email
            )), Times.Once);
        }
    }
}