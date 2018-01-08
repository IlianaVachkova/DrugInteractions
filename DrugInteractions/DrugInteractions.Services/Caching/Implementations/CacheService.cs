using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace DrugInteractions.Services.Caching.Implementations
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache cache;

        public CacheService(IMemoryCache cache)
        {
            this.cache = cache;
        }

        public T Get<T>(string cacheID, Func<T> getItemCallback) where T : class
        {
            T item = this.cache.Get(cacheID) as T;
            if (item == null)
            {
                item = getItemCallback();
                this.cache.Set(cacheID, item, DateTimeOffset.Now.AddHours(4));
            }
            return item;
        }

        public void Clear(string cacheId)
        {
            this.cache.Remove(cacheId);
        }
    }
}
