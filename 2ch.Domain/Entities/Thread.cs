using Microsoft.AspNetCore.Http;

namespace _2ch.Domain.Entities
{
    public class Thread
    {
        public Guid ThreadId { get; set; }
        public Guid BoardId { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
