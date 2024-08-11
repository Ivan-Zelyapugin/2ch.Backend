using _2ch.Application.Interfaces;
using _2ch.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace _2ch.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnonymousUserController : ControllerBase
    {
        private readonly IAnonymousUserService _anonymousUserService;
        private readonly IHashingService _hashingService;

        public AnonymousUserController(IAnonymousUserService anonymousUserService, IHashingService hashingService)
        {
            _anonymousUserService = anonymousUserService;
            _hashingService = hashingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrCreateUser()
        {
            var ipAddress = Request.HttpContext.Connection.RemoteIpAddress?.ToString();
            var salt = _hashingService.GenerateDeterministicSalt(ipAddress);
            var userHash = _hashingService.GenerateHash(ipAddress, salt);

            var user = await _anonymousUserService.GetUserByHashAsync(userHash);
            if (user == null)
            {
                user = new AnonymousUser { UserId = Guid.NewGuid(), Hash = userHash };
                await _anonymousUserService.AddUserAsync(user);
            }

            return Ok(user);
        }
    }
}
