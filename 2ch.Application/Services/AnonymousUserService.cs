using _2ch.Application.Interfaces;
using _2ch.Application.Repositories;
using _2ch.Domain.Entities;

namespace _2ch.Application.Services
{
    public class AnonymousUserService : IAnonymousUserService
    {
        private readonly IAnonymousUserRepository _anonymousUserRepository;

        public AnonymousUserService(IAnonymousUserRepository anonymousUserRepository) =>
            _anonymousUserRepository = anonymousUserRepository;

        public async Task<AnonymousUser> GetUserByIdAsync(Guid userId)
        {
            return await _anonymousUserRepository.GetUserByIdAsync(userId);
        }

        public async Task<AnonymousUser> GetUserByHashAsync(string hash)
        {
            return await _anonymousUserRepository.GetUserByHashAsync(hash);
        }

        public async Task AddUserAsync(AnonymousUser user)
        {
            await _anonymousUserRepository.AddUserAsync(user);
        }
    }
}
