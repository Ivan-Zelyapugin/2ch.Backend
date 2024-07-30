using _2ch.Application.Interfaces.DbConnection;
using _2ch.Application.Interfaces.Repositories;
using _2ch.Domain.Entities;
using Dapper;

namespace _2ch.Persistence.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public CommentRepository(IDbConnectionFactory connectionFactory) =>
            _connectionFactory = connectionFactory;

        public async Task<IEnumerable<Comment>> GetAllComments(Guid threadId)
        {
            var query = "SELECT * FROM Comments WHERE \"ThreadId\" = @ThreadId";
            using (var connection = _connectionFactory.CreateConnection())
            {
                return await connection.QueryAsync<Comment>(query, new { ThreadId = threadId });
            }
        }

        public async Task<Comment> GetCommentById(Guid id)
        {
            var query = "SELECT * FROM Comments WHERE \"Id\" = @Id";
            using (var connection = _connectionFactory.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Comment>(query, new { Id = id });
            }
        }

        public async Task CreateComment(Comment comment)
        {
            var query = @"
                INSERT INTO Comments (ThreadId, ParentCommentId, Content, CreatedAt)
                VALUES (@ThreadId, @ParentCommentId, @Content, @CreatedAt)";
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(query, comment);
            }
        }

        public async Task UpdateComment(Comment comment)
        {
            var query = @"
                UPDATE Comments
                SET Content = @Content, CreatedAt = @CreatedAt
                WHERE Id = @Id";
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(query, comment);
            }
        }

        public async Task DeleteComment(Guid id)
        {
            var query = "DELETE FROM Comments WHERE Id = @Id";
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { Id = id });
            }
        }   
    }
}
