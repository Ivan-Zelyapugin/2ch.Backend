using _2ch.Application.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using DomainThread = _2ch.Domain.Entities.Thread;

namespace _2ch.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThreadsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ThreadsController(IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;

        [HttpGet("{boardId}")]
        public async Task<IActionResult> GetThreads(Guid boardId)
        {
            var threads = await _unitOfWork.Threads.GetAllThreads(boardId);
            return Ok(threads);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetThread(Guid id)
        {
            var thread = await _unitOfWork.Threads.GetThreadById(id);
            if (thread == null)
                return NotFound();

            return Ok(thread);
        }

        [HttpPost]
        public async Task<IActionResult> CreateThread(DomainThread thread)
        {
            await _unitOfWork.Threads.CreateThread(thread);
            return CreatedAtAction(nameof(GetThread), new { id = thread.Id }, thread);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateThread(Guid id, DomainThread thread)
        {
            if (id != thread.Id)
                return BadRequest();

            await _unitOfWork.Threads.UpdateThread(thread);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteThread(Guid id)
        {
            await _unitOfWork.Threads.DeleteThread(id);
            return NoContent();
        }
    }
}
