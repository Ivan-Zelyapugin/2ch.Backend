using _2ch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2ch.Application.Interfaces.Repositories
{
    public interface IBoardRepository
    {
        Task<IEnumerable<Board>> GetAllBoards();
        Task<Board> GetBoardById(Guid id);
        Task CreateBoard(Board board);
        Task UpdateBoard(Board board);
        Task DeleteBoard(Guid id);
    }
}
