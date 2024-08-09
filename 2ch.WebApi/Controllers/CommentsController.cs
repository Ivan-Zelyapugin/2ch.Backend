using _2ch.Application.DTOs;
using _2ch.Application.Interfaces;
using _2ch.Application.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Minio;
using Minio.DataModel.Args;

namespace _2ch.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMinioClient _minioClient;
        private const string _bucketName = "filestorage";

        public CommentsController(ICommentService commentService, IMinioClient minioClient)
        {
            _commentService = commentService;
            _minioClient = minioClient;
        }
            

        [HttpGet]
        public async Task<IActionResult> GetComments(Guid threadId)
        {
            var comments = await _commentService.GetAllCommentAsync(threadId);
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
        public async Task<IActionResult> AddComment(Guid id, [FromForm] CommentDTO commentDTO, IFormFile? file)
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

                commentDTO.FilePath = $"/{_bucketName}/{objectName}";
            }

            await _commentService.AddCommentAsync(id, commentDTO, userId);
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
            var comment = await _commentService.GetCommentByIdAsync(id);

            string filePath = comment.FilePath;
            if (!string.IsNullOrEmpty(filePath))
            {
                string fileName = Path.GetFileName(filePath);
                if (!string.IsNullOrEmpty(fileName))
                {
                    await _minioClient.RemoveObjectAsync(new RemoveObjectArgs().WithBucket(_bucketName).WithObject(fileName));
                }
            }

            await _commentService.DeleteAsync(id);
            return NoContent();
        }
    }
}
