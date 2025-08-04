using Blog.Domain.General;

namespace Blog.Domain.Entity
{
    public class PublicationEntity : BaseEntity
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        
        public UserEntity? Users { get; set; }
        public int UserId { get; set; }
    }
}