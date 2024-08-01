using _2ch.Application.DTOs;
using _2ch.Application.Interfaces;
using _2ch.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace _2ch.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardsController : ControllerBase
    {
        private readonly IBoardService _boardService;
        public BoardsController(IBoardService boardService) =>
            _boardService = boardService;

        [HttpGet]
        public async Task<IActionResult> GetAllBoards()
        {
            var boards = await _boardService.GetAllBoardsAsync();
            return Ok(boards);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBoardById(Guid id)
        {
            var board = await _boardService.GetBoardByIdAsync(id);
            if (board == null)
            {
                return NotFound();
            }
            return Ok(board);
        }

        [HttpPost]
        public async Task<IActionResult> AddBoard(BoardDto boardDto)
        {
            await _boardService.AddBoardAsync(boardDto);
            return Ok(boardDto);        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBoard(Guid id, BoardDto boardDto)
        {
            await _boardService.UpdateBoardAsync(boardDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoard(Guid id)
        {
            await _boardService.DeleteBoardAsync(id);
            return NoContent();
        }
    }
}
