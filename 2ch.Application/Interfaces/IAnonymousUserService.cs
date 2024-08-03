using _2ch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2ch.Application.Interfaces
{
    public interface IAnonymousUserService
    {
        Task<AnonymousUser> GetUserByIdAsync(Guid userId);
        Task AddUserAsync(AnonymousUser user);
    }
}
