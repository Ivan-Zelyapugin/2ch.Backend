using _2ch.Application.Interfaces.DbConnection;
using _2ch.Application.Interfaces.Repositories;
using Dapper;
using DomainThread = _2ch.Domain.Entities.Thread;

namespace _2ch.Persistence.Repositories
{
    public class ThreadRepository : IThreadRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public ThreadRepository(IDbConnectionFactory connectionFactory) =>
            _connectionFactory = connectionFactory;

        public async Task<IEnumerable<DomainThread>> GetAllThreads(Guid boardId)
        {
            var query = "SELECT * FROM Threads WHERE \"BoardId\" = @BoardId";
            using (var connection = _connectionFactory.CreateConnection())
            {
                return await connection.QueryAsync<DomainThread>(query, new { BoardId = boardId });
            }
        }

        public async Task<DomainThread> GetThreadById(Guid id)
        {
            var query = "SELECT * FROM Threads WHERE \"Id\" = @Id";
            using (var connection = _connectionFactory.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<DomainThread>(query, new { Id = id });
            }
        }

        public async Task CreateThread(DomainThread thread)
        {
            var query = @"
                INSERT INTO Threads (""BoardId"", ""Title"", ""Content"", ""CreatedAt"")
                VALUES (@BoardId, @Title, @Content, @CreatedAt)";
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(query, thread);
            }
        }

        public async Task UpdateThread(DomainThread thread)
        {
            var query = @"
                UPDATE Threads
                SET ""Title"" = @Title, ""Content"" = @Content, ""CreatedAt"" = @CreatedAt
                WHERE ""Id"" = @Id";
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(query, thread);
            }
        }

        public async Task DeleteThread(Guid id)
        {
            var query = "DELETE FROM Threads WHERE Id = @Id";
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { Id = id });
            }
        }    
    }
}
