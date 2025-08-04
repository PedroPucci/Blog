using Blog.Domain.Entity;

namespace Blog.Test.Entity
{
    public class PublicationEntityTest
    {
        [Fact]
        public void PublicationEntity_ShouldSetAndGetPropertiesCorrectly()
        {
            // Arrange
            var user = new UserEntity
            {
                Name = "Pedro",
                Email = "pedro@example.com",
                Password = "123456"
            };

            var publication = new PublicationEntity
            {
                Title = "Test Title",
                Content = "Test Content",
                UserId = 42,
                Users = user
            };

            // Assert
            Assert.Equal("Test Title", publication.Title);
            Assert.Equal("Test Content", publication.Content);
            Assert.Equal(42, publication.UserId);
            Assert.NotNull(publication.Users);
            Assert.Equal("Pedro", publication.Users.Name);
            Assert.Equal("pedro@example.com", publication.Users.Email);
        }
    }
}