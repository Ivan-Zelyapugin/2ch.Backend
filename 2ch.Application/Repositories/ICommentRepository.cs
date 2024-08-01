using _2ch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2ch.Application.Repositories
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAllCommentsAsync();
        Task<Comment> GetCommentByIdAsync(Guid id);
        Task AddCommentAsync(Comment comment);
        Task UpdateCommentAsync(Comment comment);
        Task DeleteCommentAsync(Guid id);
    }
}
