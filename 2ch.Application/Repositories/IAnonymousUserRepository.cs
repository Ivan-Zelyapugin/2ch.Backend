using _2ch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2ch.Application.Repositories
{
    public interface IAnonymousUserRepository
    {
        Task<AnonymousUser> GetUserByIdAsync(Guid userId);
        Task AddUserAsync(AnonymousUser user);
    }
}
