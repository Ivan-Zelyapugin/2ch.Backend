using _2ch.Application.Interfaces;
using _2ch.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using _2ch.Application.Services;
using Microsoft.AspNetCore.Cors;

namespace _2ch.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnonymousUserController : ControllerBase
    {
        private readonly IAnonymousUserService _anonymousUserService;
        private readonly RedisCacheService _redisCacheService;

        public AnonymousUserController(IAnonymousUserService anonymousUserService, RedisCacheService redisCacheService)
        {
            _anonymousUserService = anonymousUserService;
            _redisCacheService = redisCacheService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrCreateUser()
        {
            Guid userId;
            if (!Request.Cookies.TryGetValue("UserId", out var userIdStr) || !Guid.TryParse(userIdStr, out userId))
            {
                userId = Guid.NewGuid();
                Response.Cookies.Append("UserId", userId.ToString(), new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTimeOffset.UtcNow.AddYears(1)
                });

                var user = new AnonymousUser { UserId = userId };
                await _anonymousUserService.AddUserAsync(user);
                await _redisCacheService.SetCacheValueAsync(userId.ToString(), userId.ToString());
            }
            else
            {
                var cachedUser = await _redisCacheService.GetCacheValueAsync(userId.ToString());
                if (string.IsNullOrEmpty(cachedUser))
                {
                    var existingUser = await _anonymousUserService.GetUserByIdAsync(userId);

                    if (existingUser == null)
                    {
                        var user = new AnonymousUser { UserId = userId };
                        await _anonymousUserService.AddUserAsync(user);
                    }

                    await _redisCacheService.SetCacheValueAsync(userId.ToString(), userId.ToString());
                }
            }

            var createdUser = await _anonymousUserService.GetUserByIdAsync(userId);
            return Ok(createdUser);
        }
    }
}
