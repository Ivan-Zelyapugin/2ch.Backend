using _2ch.Application.DbConnection;
using _2ch.Application.DTOs;
using _2ch.Application.Repositories;
using Dapper;
using DomainThread = _2ch.Domain.Entities.Thread;

namespace _2ch.Persistence.Repositories
{
    public class ThreadRepository : IThreadRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public ThreadRepository(IDbConnectionFactory connectionFactory) =>
            _connectionFactory = connectionFactory;

        public async Task<IEnumerable<DomainThread>> GetAllThreadsAsync()
        {
            var sql = "SELECT * FROM thread";
            using (var connection = _connectionFactory.CreateConnection())
            {
                return await connection.QueryAsync<DomainThread>(sql);
            }
        }

        public async Task<DomainThread> GetThreadByIdAsync(Guid threadId)
        {
            var query = "SELECT * FROM thread WHERE \"ThreadId\" = @ThreadId";
            using (var connection = _connectionFactory.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<DomainThread>(query, new { ThreadId = threadId });
            }
        }

        public async Task AddThreadAsync(DomainThread thread)
        {
            var sql = @"
                INSERT INTO thread (""ThreadId"", ""BoardId"", ""Title"", ""Content"", ""CreatedAt"")
                VALUES (@ThreadId, @BoardId, @Title, @Content, @CreatedAt)";
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(sql, thread);
            }
        }

        public async Task UpdateThreadAsync(DomainThread thread)
        {
            var sql = @"
                UPDATE thread
                SET ""Title"" = @Title, ""Content"" = @Content, ""CreatedAt"" = @CreatedAt
                WHERE ""ThreadId"" = @ThreadId";
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(sql, thread);
            }
        }

        public async Task DeleteThreadAsync(Guid threadId)
        {
            var sql = "DELETE FROM thread WHERE \"ThreadId\" = @ThreadId";
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(sql, new { ThreadId = threadId });
            }
        }    
    }
}
