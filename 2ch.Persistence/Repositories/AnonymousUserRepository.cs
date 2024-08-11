using _2ch.Application.DbConnection;
using _2ch.Application.Repositories;
using _2ch.Domain.Entities;
using Dapper;


namespace _2ch.Persistence.Repositories
{
    public class AnonymousUserRepository : IAnonymousUserRepository
    {
        private readonly IDbConnectionFactory _connectionString;

        public AnonymousUserRepository(IDbConnectionFactory connectionString) =>
            _connectionString = connectionString;

        public async Task<AnonymousUser> GetUserByIdAsync(Guid userId)
        {
            var sql = "SELECT * FROM AnonymousUsers WHERE \"UserId\" = @UserId";
            using (var connection = _connectionString.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<AnonymousUser>(sql, new { UserId = userId });
            }
        }

        public async Task AddUserAsync(AnonymousUser user)
        {
            var sql = "INSERT INTO AnonymousUsers (\"UserId\", \"Hash\") VALUES (@UserId, @Hash)";
            using (var connection = _connectionString.CreateConnection())
            {
                await connection.ExecuteAsync(sql, user);
            }
        }

        public async Task<AnonymousUser> GetUserByHashAsync(string hash)
        {
            string sql = "SELECT * FROM AnonymousUsers WHERE \"Hash\" = @Hash";
            using (var connection = _connectionString.CreateConnection())
            {               
                return await connection.QueryFirstOrDefaultAsync<AnonymousUser>(sql, new { Hash = hash });
            }
        }
    }
}
