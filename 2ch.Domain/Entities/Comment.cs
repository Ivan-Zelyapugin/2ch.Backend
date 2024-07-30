using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2ch.Domain.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid ThreadId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int? ParentCommentId { get; set; }
        //public ICollection<Comment> Replies { get; set; } = new List<Comment>();
    }
}
