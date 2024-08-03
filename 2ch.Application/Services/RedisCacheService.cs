using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace _2ch.Application.Services
{
    public class RedisCacheService
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public RedisCacheService(IConfiguration configuration)
        {
            _redis = ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection"));
            _database = _redis.GetDatabase();
        }

        public async Task<string> GetCacheValueAsync(string key)
        {
            return await _database.StringGetAsync(key);
        }

        public async Task SetCacheValueAsync(string key, string value)
        {
            await _database.StringSetAsync(key, value);
        }
    }
}
