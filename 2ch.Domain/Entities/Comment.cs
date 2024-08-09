using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2ch.Domain.Entities
{
    public class Comment
    {
        public Guid CommentId { get; set; }
        public Guid ThreadId { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string? FilePath { get; set; } = string.Empty;

        // Новое поле для поддержки вложенных комментариев
        public Guid? ParentCommentId { get; set; }
    }
}
