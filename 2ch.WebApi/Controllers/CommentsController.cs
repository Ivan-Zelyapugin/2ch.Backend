using _2ch.Application.Interfaces.UnitOfWork;
using _2ch.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace _2ch.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentsController(IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;

        [HttpGet("{threadId}")]
        public async Task<IActionResult> GetComments(Guid threadId)
        {
            var comments = await _unitOfWork.Comments.GetAllComments(threadId);
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment(Guid id)
        {
            var comment = await _unitOfWork.Comments.GetCommentById(id);
            if (comment == null)
                return NotFound();

            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(Comment comment)
        {
            await _unitOfWork.Comments.CreateComment(comment);
            return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, comment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(Guid id, Comment comment)
        {
            if (id != comment.Id)
                return BadRequest();

            await _unitOfWork.Comments.UpdateComment(comment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            await _unitOfWork.Comments.DeleteComment(id);
            return NoContent();
        }
    }
}
