using _2ch.Application.DTOs;
using _2ch.Application.Interfaces;
using Azure.Core;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Minio;
using Minio.DataModel.Args;
using System.Security.AccessControl;

namespace _2ch.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThreadsController : ControllerBase
    {
        private readonly IThreadService _threadService;
        private readonly IMinioClient _minioClient;
        private const string _bucketName = "filestorage";

        public ThreadsController(IMinioClient minioClient, IThreadService threadService)
        {
            _minioClient = minioClient;
            _threadService = threadService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllThreads(Guid boardId)
        {
            var threads = await _threadService.GetAllThreadsAsync(boardId);
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
        public async Task<IActionResult> AddThread(Guid boardId, [FromForm] ThreadDto threadDto, IFormFile? file)
        {
            if (!Request.Cookies.TryGetValue("UserId", out var userIdStr) || !Guid.TryParse(userIdStr, out var userId))
            {
                return Unauthorized();
            }

            if (file != null)
            {
                var fileName = file.FileName;
                var objectName = file.FileName;

                bool found = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(_bucketName));
                if (!found)
                {
                    await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(_bucketName));
                }

                using (var stream = file.OpenReadStream())
                {
                    await _minioClient.PutObjectAsync(new PutObjectArgs()
                        .WithBucket(_bucketName)
                        .WithObject(objectName)
                        .WithStreamData(stream)
                        .WithObjectSize(stream.Length)
                        .WithContentType(file.ContentType));
                }

                threadDto.FilePath = $"/{_bucketName}/{objectName}";
            }

            await _threadService.AddThreadAsync(boardId, threadDto, userId);
            return Ok(threadDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateThread(Guid id, ThreadDto threadDto)
        {
            await _threadService.UpdateThreadAsync(id, threadDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteThread(Guid id)
        {
            var file = await _threadService.GetThreadByIdAsync(id);

            string filePath = file.FilePath;

            if (!string.IsNullOrEmpty(filePath))
            {
                string fileName = Path.GetFileName(filePath);
                if (!string.IsNullOrEmpty(fileName))
                {
                    await _minioClient.RemoveObjectAsync(new RemoveObjectArgs().WithBucket(_bucketName).WithObject(fileName));
                }
            }         
            
            await _threadService.DeleteThreadAsync(id);

            return NoContent();
        }
    }
}
