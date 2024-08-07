using _2ch.Application.DTOs;
using _2ch.Domain.Entities;

namespace _2ch.Application.Interfaces
{
    public interface IBoardService
    {
        Task<IEnumerable<Board>> GetAllBoardsAsync();
        Task<BoardDto> GetBoardByIdAsync(Guid boardId);
        Task AddBoardAsync(BoardDto boardDto, Guid id);
        Task UpdateBoardAsync(Guid id, BoardDto boardDto);
        Task DeleteBoardAsync(Guid boardId);
    }
}
