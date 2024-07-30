using _2ch.Application.Interfaces.UnitOfWork;
using _2ch.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace _2ch.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public BoardsController(IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;

        [HttpGet]
        public async Task<IActionResult> GetBoards()
        {
            var boards = await _unitOfWork.Boards.GetAllBoards();
            return Ok(boards);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBoard(Guid id)
        {
            var board = await _unitOfWork.Boards.GetBoardById(id);
            if (board == null)
                return NotFound();

            return Ok(board);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBoard(Board board)
        {
            await _unitOfWork.Boards.CreateBoard(board);
            return CreatedAtAction(nameof(GetBoard), new { id = board.Id }, board);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBoard(Guid id, Board board)
        {
            if (id != board.Id)
                return BadRequest();

            await _unitOfWork.Boards.UpdateBoard(board);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoard(Guid id)
        {
            await _unitOfWork.Boards.DeleteBoard(id);
            return NoContent();
        }
    }
}
