using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Harbinton.API.Application.Extensions
{
    public class ExtensionCache: IExtensionCache
    {
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        public async Task<T> GetRecordAsync<T>(IMemoryCache cache, string recordKey)
        {
            try
            {
                await semaphore.WaitAsync();
                if (cache != null)
                {
                    if(cache.TryGetValue(recordKey, out string cacheValue) != false)
                    {
                        return JsonConvert.DeserializeObject<T>(cacheValue);
                    }
                }
                return default(T);

            }
            finally
            {
                semaphore.Release();
            }
        }

        public async Task RemoveRecordAsync(IMemoryCache cache, string recordKey)
        {
            try
            {
                await semaphore.WaitAsync();
                if(cache != null)
                {
                    if (cache.TryGetValue(recordKey, out string cacheValue) != false)
                    {
                        cache.Remove(recordKey);
                    }
                }
            }
            finally
            {
                semaphore.Release();
            }
        }

        public async Task SetRecordAsync<T>(IMemoryCache cache, T entity, string recordKey, TimeSpan? expiration = null, TimeSpan? absoluteTime = null)
        {
            try
            {
                await semaphore.WaitAsync();
                if (cache != null)
                {
                    MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
                    options.AbsoluteExpirationRelativeToNow = absoluteTime ?? TimeSpan.FromSeconds(600);
                    options.SlidingExpiration = expiration ?? TimeSpan.FromSeconds(600);
                    var jsonData = JsonConvert.SerializeObject(entity);
                    cache.Set(recordKey, jsonData, options);
                }
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}
