using _2ch.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2ch.Application.Interfaces
{
    public interface IThreadService
    {
        Task<IEnumerable<ThreadDto>> GetAllThreadsAsync();
        Task<ThreadDto> GetThreadByIdAsync(Guid threadId);
        Task AddThreadAsync(Guid id, ThreadDto threadDto);
        Task UpdateThreadAsync(Guid id, ThreadDto threadDto);
        Task DeleteThreadAsync(Guid threadId);
    }
}
