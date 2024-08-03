using _2ch.Application.DTOs;

namespace _2ch.Application.Interfaces
{
    public interface IBoardService
    {
        Task<IEnumerable<BoardDto>> GetAllBoardsAsync();
        Task<BoardDto> GetBoardByIdAsync(Guid boardId);
        Task AddBoardAsync(BoardDto boardDto, Guid id);
        Task UpdateBoardAsync(Guid id, BoardDto boardDto);
        Task DeleteBoardAsync(Guid boardId);
    }
}
