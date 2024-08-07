using _2ch.Application.DTOs;
using _2ch.Application.Interfaces;
using _2ch.Application.Repositories;
using DomainTread = _2ch.Domain.Entities.Thread;

namespace _2ch.Application.Services
{
    public class ThreadService : IThreadService
    {
        private readonly IThreadRepository _threadRepository;

        public ThreadService(IThreadRepository threadRepository) =>
            _threadRepository = threadRepository;

        public async Task<IEnumerable<DomainTread>> GetAllThreadsAsync(Guid boardId)
        {
            var threads = await _threadRepository.GetAllThreadsAsync(boardId);
            return threads.Select(t => new DomainTread
            {
                ThreadId = t.ThreadId,
                BoardId = t.BoardId,
                UserId = t.UserId,
                Title = t.Title,
                Content = t.Content,
                CreatedAt = t.CreatedAt,
                FilePath = t.FilePath
            });
        }

        public async Task<ThreadDto> GetThreadByIdAsync(Guid threadId)
        {
            var thread = await _threadRepository.GetThreadByIdAsync(threadId);
            if (thread == null)
            {
                return null;
            }
            return new ThreadDto
            {
                Title = thread.Title,
                Content = thread.Content
            };
        }

        public async Task AddThreadAsync(Guid id, ThreadDto threadDto, Guid userId)
        {
            var thread = new DomainTread
            {
                ThreadId = Guid.NewGuid(),
                BoardId = id,
                UserId = userId,
                Title = threadDto.Title,
                Content = threadDto.Content,
                CreatedAt = DateTime.UtcNow,
                FilePath = threadDto.FilePath
            };
            await _threadRepository.AddThreadAsync(thread);
        }

        public async Task UpdateThreadAsync(Guid id, ThreadDto threadDto)
        {
            var thread = new DomainTread
            {
                ThreadId = id,
                Title = threadDto.Title,
                Content = threadDto.Content
            };
            await _threadRepository.UpdateThreadAsync(thread);
        }

        public async Task DeleteThreadAsync(Guid threadId)
        {
            await _threadRepository.DeleteThreadAsync(threadId);
        }
    }
}
