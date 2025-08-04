using Blog.Domain.General;
using System.Text.Json.Serialization;

namespace Blog.Domain.Entity
{
    public class PublicationEntity : BaseEntity
    {
        public string? Title { get; set; }
        public string? Content { get; set; }

        [JsonIgnore]
        public UserEntity? Users { get; set; }
        public int UserId { get; set; }
    }
}