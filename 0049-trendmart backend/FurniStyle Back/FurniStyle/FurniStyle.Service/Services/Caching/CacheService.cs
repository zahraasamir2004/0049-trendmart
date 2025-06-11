using FurniStyle.Core.IServices;
using FurniStyle.Core.IServices.Caching;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FurniStyle.Service.Services.Caching
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _database;
        public CacheService(IConnectionMultiplexer redis)
        {
            _database= redis.GetDatabase();
            
        }
        public async Task<string> GetCacheKeyAsync(string cacheKey)
        {
            var cacheResponde= await _database.StringGetAsync(cacheKey);
            if (cacheResponde.IsNullOrEmpty) return null;
            return cacheResponde.ToString();
        }

        public async Task SetCacheKeyAsync(string cacheKey, object response, TimeSpan expireTime)
        {
            if (response is null) return;
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            await _database.StringSetAsync(cacheKey,JsonSerializer.Serialize( response, options), expireTime);

        }
    }
}
