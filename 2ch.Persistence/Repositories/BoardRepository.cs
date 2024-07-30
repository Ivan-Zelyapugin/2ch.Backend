using _2ch.Application.Interfaces.DbConnection;
using _2ch.Application.Interfaces.Repositories;
using _2ch.Domain.Entities;
using Dapper;

namespace _2ch.Persistence.Repositories
{
    public class BoardRepository : IBoardRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public BoardRepository(IDbConnectionFactory connectionFactory) =>
            _connectionFactory = connectionFactory;

        public async Task<IEnumerable<Board>> GetAllBoards()
        {
            var query = "SELECT * FROM Boards";
            using (var connection = _connectionFactory.CreateConnection())
            {
                return await connection.QueryAsync<Board>(query);
            }
        }

        public async Task<Board> GetBoardById(Guid id)
        {
            var query = "SELECT * FROM Boards WHERE \"Id\" = @Id";
            using (var connection = _connectionFactory.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Board>(query, new { Id = id });
            }
        }

        public async Task CreateBoard(Board board)
        {
            var query = @"
                INSERT INTO Boards (""Name"", ""Description"", ""PostsPerHour"", ""UniqueIPs"", ""TotalPosts"")
                VALUES (@Name, @Description, @PostsPerHour, @UniqueIPs, @TotalPosts)";
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(query, board);
            }

        }

        public async Task UpdateBoard(Board board)
        {
            var query = @"
                UPDATE Boards
                SET ""Name"" = @Name, ""Description"" = @Description, ""PostsPerHour"" = @PostsPerHour,
                    ""UniqueIPs"" = @UniqueIPs, ""TotalPosts"" = @TotalPosts
                WHERE ""Id"" = @Id";
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(query, board);
            }
        }
        public async Task DeleteBoard(Guid id)
        {
            var query = "DELETE FROM Boards WHERE \"Id\" = @Id";
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { Id = id });
            }
        }   
    }
}
