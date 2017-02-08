using CacheManager.Core;

namespace RedisClient
{
    public class SimpleRedisClient
    {
        private readonly ICacheManager<ComplexCacheItem> _cache;

        public SimpleRedisClient(string host, int port)
        {
            _cache = CacheFactory.Build<ComplexCacheItem>(settings =>
            {
                settings
                    .WithRedisConfiguration("redis", config =>
                    {
                        config.WithAllowAdmin()
                            .WithDatabase(0)
                            .WithEndpoint(host, port);
                    })
                    .WithMaxRetries(1000)
                    .WithRetryTimeout(100)
                    .WithRedisBackplane("redis")
                    .WithRedisCacheHandle("redis", true);
            });
        }

        public void DoJob()
        {
            //Add item to cache
            var cacheItem = new ComplexCacheItem();
            _cache.Add("myKey", cacheItem);


            //Get item from cache
            ComplexCacheItem itemFromCache = _cache.Get("myKey");
            itemFromCache.ChildObject.CacheVisitCount++;

            //Update item in cache.
            _cache.Update("myKey", item => itemFromCache);

            //Check if modification is done
            itemFromCache = _cache.Get("myKey");


            //Remove item from cache.
            _cache.Remove("myKey");

            //Check if item removed
            itemFromCache = _cache.Get("myKey");

        }

    }
}