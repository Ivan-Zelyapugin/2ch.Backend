using Microsoft.AspNetCore.Http;

namespace _2ch.Domain.Entities
{
    public class Thread
    {
        public Guid Id { get; set; }
        public Guid BoardId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public IFormFile File { get; set; }
        //public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
