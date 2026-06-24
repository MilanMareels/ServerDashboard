using ServerDashboardApi.Context;
using ServerDashboardApi.Models;
using System;
using System.IO.Ports;
using System.Text.Json;
using System.Threading;

namespace ServerDashboardApi.Services
{
    public class SerialReaderService:BackgroundService
    {
        private ILogger<SerialReaderService> _logger;
        private const string PortName = "/dev/ttyACM0";
        private readonly IHostEnvironment _env;
        private DateTime _lastSaveTime = DateTime.MinValue;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly LiveTemperatureCaching _cacheService;

        public SerialReaderService(ILogger<SerialReaderService> logger, IHostEnvironment env, IServiceScopeFactory scopeFactory, LiveTemperatureCaching cacheService)
        {
            _logger = logger;
            _env = env;
            _scopeFactory = scopeFactory;
            _cacheService = cacheService;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            if (_env.IsDevelopment()) await RunSimulationTemps(cancellationToken);
            else await RunRealDeviceTemps(cancellationToken);
        }

        private async Task RunSimulationTemps(CancellationToken cancellationToken)
        {
            var random = new Random();
            int minTemp = int.MaxValue;
            int maxTemp = int.MinValue;

            while (!cancellationToken.IsCancellationRequested)
            {
                int temp = random.Next(20,32); // For local dev.
                var data = $"Back: {(temp > 25 ? "ON" : "OFF")}, Top: {(temp > 30 ? "ON":"OFF")}";

                if(temp < minTemp) minTemp = temp;
                if(temp > maxTemp) maxTemp = temp;

                var metrics = new CachedSensorMetrics // Object to caching
                {
                    Temp = temp,
                    MinTemp = minTemp,
                    MaxTemp = maxTemp,
                    BackFans = $"{(temp > 25 ? "ON" : "OFF")}",
                    TopAndBottomFans = $"{(temp > 30 ? "ON" : "OFF")}"
                };

                var microBit = new MicroBit // Object to db
                {
                    Temp = temp,
                    BackFans = metrics.BackFans,
                    TopAndBottomFans = metrics.TopAndBottomFans,
                };

                _cacheService.SetTemperatureCache(metrics); // Caching

                await SaveDataToDb(microBit, cancellationToken); // DB

                _logger.LogInformation($"{data}");

                await Task.Delay(1000, cancellationToken);
            }
        }

        private async Task RunRealDeviceTemps(CancellationToken cancellationToken)
        {
            int minTemp = int.MaxValue;
            int maxTemp = int.MinValue;

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    using var usb = new SerialPort(PortName)
                    {
                        BaudRate = 115200,
                        DtrEnable = true,
                        RtsEnable = true
                    };
                    usb.Open();
                    _logger.LogInformation($"Verbonden met Micro:bit op {PortName}");

                    while (!cancellationToken.IsCancellationRequested)
                    {
                        string line = usb.ReadLine();
                        var data = JsonSerializer.Deserialize<MicroBit>(line, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                        if (data == null) continue;

                        if (data.Temp < minTemp) minTemp = data.Temp;
                        if (data.Temp > maxTemp) maxTemp = data.Temp;

                        var metrics = new CachedSensorMetrics
                        {
                            Temp = data.Temp,
                            MinTemp = minTemp,
                            MaxTemp = maxTemp,
                            BackFans = data.BackFans,
                            TopAndBottomFans = data.TopAndBottomFans
                        };

                        _logger.LogInformation($"Nieuwe temperatuur gelezen: {data.Temp}°C");
                        _cacheService.SetTemperatureCache(metrics);
                        await SaveDataToDb(data, cancellationToken);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning($"Probleem met USB verbinding: {ex.Message}. Opnieuw proberen in 5 seconden...");
                    await Task.Delay(5000, cancellationToken);
                }
            }
        }

        // Save to DB helper. Save to DB between 5 min.
        private async Task SaveDataToDb(MicroBit microBit, CancellationToken cancellationToken)
        {
            bool isEvent = microBit.Temp > 30;

            if (DateTime.UtcNow >= _lastSaveTime.AddMinutes(5))
            {
                using var scope = _scopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<DashBoardContext>();

                db.Tempertures.Add(new Temperture { Date = DateTime.UtcNow, Temp = microBit.Temp, BackFans = microBit.BackFans, TopAndBottomFans = microBit.TopAndBottomFans });

                if (isEvent)
                {
                    db.Events.Add(new Event { Date = DateTime.UtcNow, Temp = microBit.Temp, Severity = microBit.Temp > 40 ? "Critical" : "Warning" });
                }

                await db.SaveChangesAsync(cancellationToken);
                _lastSaveTime = DateTime.UtcNow;
            }
        }
    }
}
