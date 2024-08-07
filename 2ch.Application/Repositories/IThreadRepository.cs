using System;
using DomainThread = _2ch.Domain.Entities.Thread;

namespace _2ch.Application.Repositories
{
    public interface IThreadRepository
    {
        Task<IEnumerable<DomainThread>> GetAllThreadsAsync(Guid boardId);
        Task<DomainThread> GetThreadByIdAsync(Guid id);
        Task AddThreadAsync(DomainThread thread);
        Task UpdateThreadAsync(DomainThread thread);
        Task DeleteThreadAsync(Guid id);
    }
}
