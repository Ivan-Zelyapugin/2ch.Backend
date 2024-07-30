using _2ch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2ch.Application.Interfaces.Repositories
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAllComments(Guid threadId);
        Task<Comment> GetCommentById(Guid id);
        Task CreateComment(Comment comment);
        Task UpdateComment(Comment comment);
        Task DeleteComment(Guid id);
    }
}
