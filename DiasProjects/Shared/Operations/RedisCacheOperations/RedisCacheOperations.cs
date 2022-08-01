using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace DiasWebApi.Shared.Operations.RedisCacheOperation
{
    public static class RedisCacheOperations<TDto> where TDto : class
    {
        public async static Task<TDto> GetFromCache(IDistributedCache cache, string key) 
        {
            string cachedResponse = await cache.GetStringAsync(key);
            return (cachedResponse == null) ? null : JsonSerializer.Deserialize<TDto>(cachedResponse);
        }

        public async static Task<IEnumerable<TDto>> GetFromCacheViaList(IDistributedCache cache, string key)
        {
            string cachedResponse = await cache.GetStringAsync(key);
            return (cachedResponse == null) ? null : JsonSerializer.Deserialize<IEnumerable<TDto>>(cachedResponse);
        }

        /// <summary>
        /// Tek Dto değerlerinde kullanılmalıdır
        /// </summary>       
        public async static Task SetCache(IDistributedCache cache, string key, TDto value, DistributedCacheEntryOptions options)
        {
            string response = JsonSerializer.Serialize(value);
            await cache.SetStringAsync(key, response, options);
        }

        /// <summary>
        /// Liste Dto değerlerinde kullanılmalıdır
        /// </summary>       
        public async static Task SetCacheList(IDistributedCache cache, string key,
                                                IEnumerable<TDto> value, DistributedCacheEntryOptions options)
        {
            string response = JsonSerializer.Serialize(value);
            await cache.SetStringAsync(key, response, options);
        }

        public async static Task ClearCache(IDistributedCache cache, string key)
        {
            await cache.RemoveAsync(key);
        }
    }
}
