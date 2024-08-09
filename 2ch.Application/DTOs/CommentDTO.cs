using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2ch.Application.DTOs
{
    public class CommentDTO
    {
        public string Content { get; set; } = string.Empty;
        public string? FilePath { get; set; } = string.Empty;
        public Guid? ParentCommentId { get; set; }
    }
}
