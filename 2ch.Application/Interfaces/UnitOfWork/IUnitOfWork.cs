using _2ch.Application.Interfaces.Repositories;
using _2ch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2ch.Application.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        IBoardRepository Boards { get; }
        IThreadRepository Threads { get; }
        ICommentRepository Comments { get; }
        //Task<int> CompleteAsync();
    }
}
