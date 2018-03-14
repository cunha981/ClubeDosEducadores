using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Text.RegularExpressions;

namespace Helper.CacheHelper
{
    public partial class CacheManager : ICacheManager
    {
        protected ObjectCache Cache
        {
            get
            {
                return MemoryCache.Default;
            }
        }

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key.</returns>
        public T Get<T>(object key)
        {
            var stringKey = string.Format("{0}-{1}", typeof(T).Name, key);
            return (T)Cache[stringKey];
        }

        public IEnumerable<T> GetAll<T>()
            where T : class
        {
            foreach (var obj in Cache)
            {
                var result = obj as T;
                if (result != null)
                {
                    yield return result;
                }
            }
        }

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">Data</param>
        /// <param name="cacheTime">Cache time</param>
        public void Set(object key, object data, int cacheTime = int.MaxValue)
        {
            if (data == null)
                return;

            var stringKey = string.Format("{0}-{1}", data.GetType().Name, key);

            var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime);
            Cache.Add(new CacheItem(stringKey, data), policy);
        }

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">Data</param>
        /// <param name="cacheTime">Cache time</param>
        public void Set<T>(object key, T data, int cacheTime)
        {
            if (data == null)
                return;

            var stringKey = string.Format("{0}-{1}", typeof(T).Name, key);

            var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime);
            Cache.Add(new CacheItem(stringKey, data), policy);
        }

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>Result</returns>
        public bool IsSet<T>(object key)
        {
            var stringKey = string.Format("{0}-{1}", typeof(T).Name, key);
            return (Cache.Contains(stringKey));
        }

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">/key</param>
        public void Remove<T>(object key)
        {
            var stringKey = string.Format("{0}-{1}", typeof(T).Name, key);
            Cache.Remove(stringKey);
        }

        private void Remove(string key)
        {
            Cache.Remove(key);
        }

        /// <summary>
        /// Removes items by pattern
        /// </summary>
        /// <param name="pattern">pattern</param>
        public void RemoveByPattern(string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = new List<string>();

            foreach (var item in Cache)
                if (regex.IsMatch(item.Key))
                    keysToRemove.Add(item.Key);

            foreach (var key in keysToRemove)
            {
                Remove(key);
            }
        }

        /// <summary>
        /// Clear all cache data
        /// </summary>
        public void Clear()
        {
            foreach (var item in Cache)
                Remove(item.Key);
        }
    }
}
