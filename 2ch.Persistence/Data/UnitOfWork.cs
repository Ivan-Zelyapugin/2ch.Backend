using _2ch.Application.Interfaces.DbConnection;
using _2ch.Application.Interfaces.Repositories;
using _2ch.Application.Interfaces.UnitOfWork;
using _2ch.Persistence.Repositories;
using System.Data;

namespace _2ch.Persistence.Data
{
    public class UnitOfWork : IUnitOfWork
    {      
        private readonly IDbConnectionFactory _connectionFactory;
        private IBoardRepository _boardRepository;
        private IThreadRepository _threadRepository;
        private ICommentRepository _commentRepository;

        public UnitOfWork(IDbConnectionFactory connectionFactory) =>
            _connectionFactory = connectionFactory;

        public IBoardRepository Boards => _boardRepository ??= new BoardRepository(_connectionFactory);
        public IThreadRepository Threads => _threadRepository ??= new ThreadRepository(_connectionFactory);
        public ICommentRepository Comments => _commentRepository ??= new CommentRepository(_connectionFactory);

        /*public void BeginTransaction()
        {
            _transaction = _connectionFactory.CreateConnection().BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                _transaction?.Commit();
            }
            catch
            {
                _transaction?.Rollback();
                throw;
            }
            finally
            {
                DisposeTransaction();
            }
        }

        public void Rollback()
        {
            try
            {
                _transaction?.Rollback();
            }
            finally
            {
                DisposeTransaction();
            }
        }

        public async Task<int> CompleteAsync()
        {
            Commit();
            return await Task.FromResult(0);
        }

        public void Dispose()
        {
            DisposeTransaction();
        }

        private void DisposeTransaction()
        {
            _transaction?.Dispose();
            _transaction = null;
        }*/
    }
}
