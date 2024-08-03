using _2ch.Application.DbConnection;
using _2ch.Application.Repositories;
using _2ch.Domain.Entities;
using Dapper;

namespace _2ch.Persistence.Repositories
{
    public class BoardRepository : IBoardRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public BoardRepository(IDbConnectionFactory connectionFactory) =>
            _connectionFactory = connectionFactory;

        public async Task<IEnumerable<Board>> GetAllBoardsAsync()
        {
            var sql = "SELECT * FROM board";
            using (var connection = _connectionFactory.CreateConnection())
            {
                return await connection.QueryAsync<Board>(sql);
            }
        }

        public async Task<Board> GetBoardByIdAsync(Guid boardId)
        {
            var sql = "SELECT * FROM board WHERE \"BoardId\" = @BoardId";
            using (var connection = _connectionFactory.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Board>(sql, new { BoardId = boardId });
            }
        }

        public async Task AddBoardAsync(Board board)
        {
            var sql = @"
                INSERT INTO board (""BoardId"", ""UserId"", ""Name"", ""Description"")
                VALUES (@BoardId, @UserId, @Name, @Description)";
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(sql, board);
            }

        }

        public async Task UpdateBoardAsync(Board board)
        {
            var sql = @"
                UPDATE board
                SET ""Name"" = @Name, ""Description"" = @Description
                WHERE ""BoardId"" = @BoardId";
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(sql, board);
            }
        }
        public async Task DeleteBoardAsync(Guid boardId)
        {
            var sql = "DELETE FROM board WHERE \"BoardId\" = @BoardId";
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(sql, new { BoardId = boardId });
            }
        }   
    }
}
