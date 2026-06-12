using Microsoft.Extensions.Caching.Memory;
using ServerDashboardApi.Models;

namespace ServerDashboardApi.Services
{
    public class LiveTemperatureCaching(IMemoryCache _memoryCache, ILogger<LiveTemperatureCaching> _logger)
    {
        private readonly string _key = "metric-data";

        public void SetTemperatureCache(CachedSensorMetrics metrics)
        {
            _memoryCache.Set(_key, metrics, new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromSeconds(10)
            });
        }

        public CachedSensorMetrics GetCurrentTemperature()
        {
            _memoryCache.TryGetValue(_key, out CachedSensorMetrics value);
            return value;
        }
    }
}
