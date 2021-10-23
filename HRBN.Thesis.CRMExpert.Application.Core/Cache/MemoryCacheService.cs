using System;
using Microsoft.Extensions.Caching.Memory;

namespace HRBN.Thesis.CRMExpert.Application.Core.Cache
{
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryCacheService"/> class.
        /// </summary>
        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Get value by key.
        /// </summary>
        public T Get<T>(string key)
            => _memoryCache.Get<T>(key);

        /// <summary>
        /// Save value in cache service.
        /// </summary>
        public void Set(string key, object value, TimeSpan expirationTime)
            => _memoryCache.Set(key, value, expirationTime);

        /// <summary>
        /// Remove value.
        /// </summary>
        public void Remove(string key)
            => _memoryCache.Remove(key);


    }
}