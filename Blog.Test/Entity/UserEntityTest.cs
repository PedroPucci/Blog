using Blog.Domain.Entity;

namespace Blog.Test.Entity
{
    public class UserEntityTest
    {
        [Fact]
        public void UserEntity_ShouldSetAndGetPropertiesCorrectly()
        {
            // Arrange
            var publications = new List<PublicationEntity>
            {
                new PublicationEntity { Title = "Post 1", Content = "Content 1" },
                new PublicationEntity { Title = "Post 2", Content = "Content 2" }
            };

            var user = new UserEntity
            {
                Name = "Pedro",
                Email = "pedro@example.com",
                Password = "123456",
                Publications = publications
            };

            // Assert
            Assert.Equal("Pedro", user.Name);
            Assert.Equal("pedro@example.com", user.Email);
            Assert.Equal("123456", user.Password);
            Assert.NotNull(user.Publications);
            Assert.Equal(2, user.Publications.Count);
            Assert.Equal("Post 1", user.Publications[0].Title);
            Assert.Equal("Content 2", user.Publications[1].Content);
        }
    }
}