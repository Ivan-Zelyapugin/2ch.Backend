using _2ch.Application.DbConnection;
using _2ch.Application.Repositories;
using _2ch.Domain.Entities;
using Dapper;

namespace _2ch.Persistence.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public CommentRepository(IDbConnectionFactory connectionFactory) =>
            _connectionFactory = connectionFactory;

        public async Task<IEnumerable<Comment>> GetAllCommentsAsync(Guid threadId)
        {
            var sql = "SELECT * FROM comment WHERE \"ThreadId\" = @ThreadId";
            using (var connection = _connectionFactory.CreateConnection())
            {
                return await connection.QueryAsync<Comment>(sql, new { ThreadId = threadId });
            }
        }

        public async Task<Comment> GetCommentByIdAsync(Guid commentId)
        {
            var sql = "SELECT * FROM comment WHERE \"CommentId\" = @CommentId";
            using (var connection = _connectionFactory.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Comment>(sql, new { CommentId = commentId });
            }
        }

        public async Task AddCommentAsync(Comment comment)
        {
            var sql = @"
                INSERT INTO comment (""CommentId"", ""ThreadId"", ""UserId"", ""Content"", ""CreatedAt"", ""filepath"", ParentCommentId)
                VALUES (@CommentId, @ThreadId, @UserId, @Content, @CreatedAt, @FilePath, @ParentCommentId)";
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(sql, comment);
            }
        }

        public async Task UpdateCommentAsync(Comment comment)
        {
            var sql = @"
                UPDATE comment
                SET ""Content"" = @Content, ""CreatedAt"" = @CreatedAt
                WHERE ""CommentId"" = @CommentId";
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(sql, comment);
            }
        }

        public async Task DeleteCommentAsync(Guid commentId)
        {
            var sql = "DELETE FROM comment WHERE \"CommentId\" = @CommentId";
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(sql, new { CommentId = commentId });
            }
        }
    }
}
