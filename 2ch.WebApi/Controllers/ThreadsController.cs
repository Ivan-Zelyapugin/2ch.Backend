using _2ch.Application.DTOs;
using _2ch.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using DomainThread = _2ch.Domain.Entities.Thread;

namespace _2ch.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThreadsController : ControllerBase
    {
        private readonly IThreadService _threadService;

        public ThreadsController(IThreadService threadService) =>
            _threadService = threadService;

        [HttpGet]
        public async Task<IActionResult> GetAllThreads()
        {
            var threads = await _threadService.GetAllThreadsAsync();
            return Ok(threads);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetThreadById(Guid id)
        {
            var thread = await _threadService.GetThreadByIdAsync(id);
            if (thread == null)
            {
                return NotFound();
            }
            return Ok(thread);
        }

        [HttpPost]
        public async Task<IActionResult> AddThread(ThreadDto threadDto)
        {
            await _threadService.AddThreadAsync(threadDto);
            return Ok(threadDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateThread(Guid id, ThreadDto threadDto)
        {
            await _threadService.UpdateThreadAsync(threadDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteThread(Guid id)
        {
            await _threadService.DeleteThreadAsync(id);
            return NoContent();
        }
    }
}
