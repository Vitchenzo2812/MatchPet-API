using MatchPet.Infrastructure.Services.Contracts;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace MatchPet.Infrastructure.Services;

public class RedisCacheService (IDistributedCache redisCache) : ICacheService
{
  public async Task SetAsync<T>(string key, T value, TimeSpan? ttl = null)
  {
    var options = new DistributedCacheEntryOptions();

    if (ttl.HasValue)
      options.AbsoluteExpirationRelativeToNow = ttl;
    
    var json = JsonSerializer.Serialize(value);
    await redisCache.SetStringAsync(key, json, options);
  }

  public async Task<T?> GetAsync<T>(string key)
  {
    var value = await redisCache.GetStringAsync(key);
    return value is null ? default : JsonSerializer.Deserialize<T>(value);
  }

  public async Task RemoveAsync(string key)
    => await redisCache.RemoveAsync(key);
}