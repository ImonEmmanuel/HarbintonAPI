using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harbinton.API.Application.Extensions
{
    public interface IExtensionCache
    {
        Task<T> GetRecordAsync<T>(IMemoryCache cache, string recordKey);
        Task SetRecordAsync<T>(IMemoryCache cache, T entity, string recordKey, 
            TimeSpan? expiration = null, TimeSpan? absoluteTime = null);
        Task RemoveRecordAsync(IMemoryCache cache, string recordKey);
    }
}
