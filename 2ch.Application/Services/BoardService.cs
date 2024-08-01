using _2ch.Application.DTOs;
using _2ch.Application.Interfaces;
using _2ch.Application.Repositories;
using _2ch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2ch.Application.Services
{
    public class BoardService : IBoardService
    {
        private readonly IBoardRepository _boardRepository;

        public BoardService(IBoardRepository boardRepository) =>
            _boardRepository = boardRepository;

        public async Task<IEnumerable<BoardDto>> GetAllBoardsAsync()
        {
            var boards = await _boardRepository.GetAllBoardsAsync();
            return boards.Select(b => new BoardDto
            {
                Name = b.Name,
                Description = b.Description
            });
        }

        public async Task<BoardDto> GetBoardByIdAsync(Guid boardId)
        {
            var board = await _boardRepository.GetBoardByIdAsync(boardId);
            if (board == null)
            {
                return null;
            }
            return new BoardDto
            {
                Name = board.Name,
                Description = board.Description
            };
        }

        public async Task AddBoardAsync(BoardDto boardDto)
        {
            var board = new Board
            {
                BoardId = Guid.NewGuid(),
                Name = boardDto.Name,
                Description = boardDto.Description
            };
            await _boardRepository.AddBoardAsync(board);
        }

        public async Task UpdateBoardAsync(BoardDto boardDto)
        {
            var board = new Board
            {
                Name = boardDto.Name,
                Description = boardDto.Description
            };
            await _boardRepository.UpdateBoardAsync(board);
        }

        public async Task DeleteBoardAsync(Guid boardId)
        {
            await _boardRepository.DeleteBoardAsync(boardId);
        }
    }
}
