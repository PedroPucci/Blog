using Blog.Application.Services;
using Blog.Domain.Dto;
using Blog.Domain.Entity;
using Blog.Infrastracture.Repository.Interfaces;
using Blog.Infrastracture.RepositoryUoW;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;

namespace Blog.Test.Service
{
    public class PublicationServiceTests
    {
        private readonly Mock<IRepositoryUoW> _repositoryUoWMock;
        private readonly Mock<IPublicationRepository> _publicationRepositoryMock;
        private readonly PublicationService _publicationService;

        public PublicationServiceTests()
        {
            _repositoryUoWMock = new Mock<IRepositoryUoW>();
            _publicationRepositoryMock = new Mock<IPublicationRepository>();

            _repositoryUoWMock.Setup(x => x.PublicationRepository).Returns(_publicationRepositoryMock.Object);
            _repositoryUoWMock.Setup(x => x.BeginTransaction()).Returns(Mock.Of<IDbContextTransaction>());

            _publicationService = new PublicationService(_repositoryUoWMock.Object);
        }

        [Fact]
        public async Task Add_ShouldReturnError_WhenValidationFails()
        {
            var publication = new PublicationEntity();

            var result = await _publicationService.Add(publication);

            Assert.False(result.Success);
            Assert.Contains("Title", result.Message);
        }

        [Fact]
        public async Task Add_ShouldReturnSuccess_WhenValidPublication()
        {
            var publication = new PublicationEntity
            {
                Title = "Valid Title",
                Content = "Some content",
                UserId = 1
            };

            _publicationRepositoryMock.Setup(x => x.Add(It.IsAny<PublicationEntity>())).ReturnsAsync(publication);
            _repositoryUoWMock.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);

            var result = await _publicationService.Add(publication);

            Assert.True(result.Success);
            _publicationRepositoryMock.Verify(x => x.Add(It.IsAny<PublicationEntity>()), Times.Once);
            _repositoryUoWMock.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task Get_ShouldReturnList_WhenCalled()
        {
            var publications = new List<PublicationEntity>
            {
                new() { Title = "Post 1", Content = "Content 1" },
                new() { Title = "Post 2", Content = "Content 2" }
            };

            _publicationRepositoryMock.Setup(x => x.Get()).ReturnsAsync(publications);
            _repositoryUoWMock.Setup(x => x.Commit());

            var result = await _publicationService.Get();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            _publicationRepositoryMock.Verify(x => x.Get(), Times.Once);
            _repositoryUoWMock.Verify(x => x.Commit(), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldRemovePublication_WhenExists()
        {
            var publication = new PublicationEntity { Title = "Delete Me", Content = "..." };

            _publicationRepositoryMock.Setup(x => x.GetById(1)).ReturnsAsync(publication);
            _publicationRepositoryMock.Setup(x => x.Delete(publication)).Verifiable();
            _repositoryUoWMock.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);

            await _publicationService.Delete(1);

            _publicationRepositoryMock.Verify(x => x.Delete(publication), Times.Once);
            _repositoryUoWMock.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldDoNothing_WhenPublicationNotFound()
        {
            _publicationRepositoryMock.Setup(x => x.GetById(1)).ReturnsAsync((PublicationEntity?)null);

            await _publicationService.Delete(1);

            _publicationRepositoryMock.Verify(x => x.Delete(It.IsAny<PublicationEntity>()), Times.Never);
            _repositoryUoWMock.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldReturnSuccess_WhenPublicationExistsAndValid()
        {
            var dto = new PublicationDto { Id = 1, Title = "Updated", Content = "Updated content" };
            var entity = new PublicationEntity { Id = 1, Title = "Old", Content = "Old" };

            _publicationRepositoryMock.Setup(x => x.GetById(dto.Id)).ReturnsAsync(entity);
            _publicationRepositoryMock.Setup(x => x.Update(It.IsAny<PublicationEntity>()));
            _repositoryUoWMock.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);

            var result = await _publicationService.Update(dto);

            Assert.True(result.Success);
            _publicationRepositoryMock.Verify(x => x.Update(It.IsAny<PublicationEntity>()), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldThrowException_WhenPublicationDoesNotExist()
        {
            var dto = new PublicationDto { Id = 999, Title = "X", Content = "Y" };

            _publicationRepositoryMock.Setup(x => x.GetById(dto.Id)).ReturnsAsync((PublicationEntity?)null);

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => _publicationService.Update(dto));

            Assert.Equal("Error updating Publication", ex.Message);
        }
    }
}