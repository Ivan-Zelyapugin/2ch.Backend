using _2ch.Application.DTOs;
using _2ch.Application.Interfaces;
using _2ch.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace _2ch.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService) =>
            _commentService = commentService;

        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            var comments = await _commentService.GetAllCommentAsync();
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment(Guid id)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            if (comment == null)
                return NotFound();

            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(Guid id, CommentDTO commentDTO)
        {
            await _commentService.AddCommentAsync(id, commentDTO);
            return Ok(commentDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(Guid id, CommentDTO commentDTO)
        {
            await _commentService.UpdateCommentAsync(id, commentDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            await _commentService.DeleteAsync(id);
            return NoContent();
        }
    }
}
