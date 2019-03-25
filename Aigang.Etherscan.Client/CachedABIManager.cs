using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace Aigang.Ethernscan.Client
{
    public static class CachedABIManager
    {
        private static TimeSpan _expirationTime = TimeSpan.FromHours(24);

        private static IMemoryCache _cache = new MemoryCache(new MemoryCacheOptions
        {
            ExpirationScanFrequency = TimeSpan.FromMinutes(30)
        });
        
        public static async Task<string> GetAbiAsync(string address)
        {
            if (_cache.TryGetValue(address, out string result))
            {
                return result;
            }

            string response = await EtherscanClient.GetABIAsync(address);

            if (!String.IsNullOrEmpty(response))
            {
                AddValue(address, response);
                return response;
            }
            
            return String.Empty;
        }

        private static void AddValue(string address, string abi)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions();
 
            cacheEntryOptions.SetAbsoluteExpiration(_expirationTime);
        
            _cache.Set(address, abi, cacheEntryOptions);
        }
        
    }
}