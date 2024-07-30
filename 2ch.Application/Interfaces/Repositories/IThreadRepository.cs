using System;
using DomainThread = _2ch.Domain.Entities.Thread;

namespace _2ch.Application.Interfaces.Repositories
{
    public interface IThreadRepository
    {
        Task<IEnumerable<DomainThread>> GetAllThreads(Guid boardId);
        Task<DomainThread> GetThreadById(Guid id);
        Task CreateThread(DomainThread thread);
        Task UpdateThread(DomainThread thread);
        Task DeleteThread(Guid id);
    }
}
