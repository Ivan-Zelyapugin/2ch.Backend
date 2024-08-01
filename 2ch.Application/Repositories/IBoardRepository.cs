using _2ch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2ch.Application.Repositories
{
    public interface IBoardRepository
    {
        Task<IEnumerable<Board>> GetAllBoardsAsync();
        Task<Board> GetBoardByIdAsync(Guid id);
        Task AddBoardAsync(Board board);
        Task UpdateBoardAsync(Board board);
        Task DeleteBoardAsync(Guid id);
    }
}
