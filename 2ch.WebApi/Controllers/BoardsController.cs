using _2ch.Application.DTOs;
using _2ch.Application.Interfaces;
using _2ch.Application.Services;
using _2ch.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace _2ch.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardsController : ControllerBase
    {
        private readonly IBoardService _boardService;
        private readonly IHashingService _hashingService;
        private readonly IAnonymousUserService _anonymousUserService;
        private readonly RedisCacheService _redisCacheService;
        public BoardsController(IAnonymousUserService anonymousUserService, IBoardService boardService, 
            IHashingService hashingService, RedisCacheService redisCacheService)
        {
            _boardService = boardService;
            _hashingService = hashingService;
            _anonymousUserService = anonymousUserService;
            _redisCacheService = redisCacheService;

        }

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
            var ipAddress = Request.HttpContext.Connection.RemoteIpAddress?.ToString();
            var salt = _hashingService.GenerateDeterministicSalt(ipAddress);
            var userHash = _hashingService.GenerateHash(ipAddress, salt);

            var cachedUser = await _redisCacheService.GetCacheValueAsync(userHash);

            AnonymousUser user;
            if (string.IsNullOrEmpty(cachedUser))
            {
                user = await _anonymousUserService.GetUserByHashAsync(userHash);

                if (user == null)
                {
                    return Unauthorized("User not found.");
                }

                await _redisCacheService.SetCacheValueAsync(userHash, user.UserId.ToString());
            }
            else
            {
                user = await _anonymousUserService.GetUserByIdAsync(Guid.Parse(cachedUser));
            }

            await _boardService.AddBoardAsync(boardDto, user.UserId);
            return Ok(boardDto);        
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBoard(Guid id, BoardDto boardDto)
        {
            await _boardService.UpdateBoardAsync(id, boardDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoard(Guid id)
        {
            await _boardService.DeleteBoardAsync(id);
            return NoContent();
        }

        private string GenerateSalt(string ipAddress)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] saltBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(ipAddress));
                return Convert.ToBase64String(saltBytes);
            }
        }

        private string GenerateHash(string ipAddress, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var combinedString = $"{ipAddress}{salt}";
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combinedString));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
