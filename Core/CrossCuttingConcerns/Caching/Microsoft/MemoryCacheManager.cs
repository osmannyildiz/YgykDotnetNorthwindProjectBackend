using Core.Utilities.Ioc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.CrossCuttingConcerns.Caching.Microsoft {
    public class MemoryCacheManager : ICacheManager {
        IMemoryCache _cache;

        public MemoryCacheManager() {
            _cache = ServiceHelper.ServiceProvider.GetService<IMemoryCache>();
        }

        public void Add(string key, object value, int expiresInMinutes) {
            _cache.Set(key, value, TimeSpan.FromMinutes(expiresInMinutes));
        }

        public T Get<T>(string key) {
            return _cache.Get<T>(key);
        }

        public object Get(string key) {
            return _cache.Get(key);
        }

        public bool Exists(string key) {
            return _cache.TryGetValue(key, out _);
        }

        public void Remove(string key) {
            _cache.Remove(key);
        }

        // Taken from https://github.com/engindemirog/NetCoreBackend/blob/master/Core/CrossCuttingConcerns/Caching/Microsoft/MemoryCacheManager.cs
        public void RemoveByPattern(string pattern) {
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_cache) as dynamic;
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();
            foreach (var cacheItem in cacheEntriesCollection) {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();
            foreach (var key in keysToRemove) {
                _cache.Remove(key);
            }
        }
    }
}
