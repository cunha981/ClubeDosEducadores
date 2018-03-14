using System;
using System.Threading.Tasks;

namespace Helper.CacheHelper
{
    public static class CacheManagerExtensions
    {
        public static void Set<T>(this ICacheManager cacheManager, object key, T data, int cacheTime = 60)
        {
            cacheManager.Set(key, data, cacheTime);
        }

        /// <summary>
        /// Get a cached item. If it's not in the cache yet, then load and cache it
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="key">Cache key</param>
        /// <param name="acquire">Function to load item if it's not in the cache yet</param>
        /// <returns>Cached item</returns>
        public static T Get<T>(this ICacheManager cacheManager, object key, Func<T> acquire)
        {
            return Get(cacheManager, key, 60, acquire);
        }

        /// <summary>
        /// Get a cached item. If it's not in the cache yet, then load and cache it
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="key">Cache key</param>
        /// <param name="cacheTime">Cache time in minutes (0 - do not cache)</param>
        /// <param name="acquire">Function to load item if it's not in the cache yet</param>
        /// <returns>Cached item</returns>
        public static T Get<T>(this ICacheManager cacheManager, object key, int cacheTime, Func<T> acquire)
        {
            if (cacheManager.IsSet<T>(key))
            {
                return cacheManager.Get<T>(key);
            }
            else
            {
                var result = acquire();
                if (cacheTime > 0)
                    cacheManager.Set<T>(key, result, cacheTime);
                return result;
            }
        }

        public static async Task<T> GetAsync<T>(this ICacheManager cacheManager, object key, int cacheTime, Func<Task<T>> acquire)
        {
            if (cacheManager.IsSet<T>(key))
            {
                return cacheManager.Get<T>(key);
            }
            else
            {
                var result = await acquire();
                if (cacheTime > 0)
                    cacheManager.Set(key, result, cacheTime);
                return result;
            }
        }
    }
}
