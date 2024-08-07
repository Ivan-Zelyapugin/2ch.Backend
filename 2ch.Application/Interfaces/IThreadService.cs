using _2ch.Application.DTOs;
using DomainTread = _2ch.Domain.Entities.Thread;
namespace _2ch.Application.Interfaces
{
    public interface IThreadService
    {
        Task<IEnumerable<DomainTread>> GetAllThreadsAsync(Guid boardId);
        Task<ThreadDto> GetThreadByIdAsync(Guid threadId);
        Task AddThreadAsync(Guid id, ThreadDto threadDto, Guid userId);
        Task UpdateThreadAsync(Guid id, ThreadDto threadDto);
        Task DeleteThreadAsync(Guid threadId);
    }
}
