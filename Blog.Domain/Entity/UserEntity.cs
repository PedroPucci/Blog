using Blog.Domain.General;
using System.Text.Json.Serialization;

namespace Blog.Domain.Entity
{
    public class UserEntity : BaseEntity
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        [JsonIgnore]
        public List<PublicationEntity> Publications { get; set; } = new();
    }
}