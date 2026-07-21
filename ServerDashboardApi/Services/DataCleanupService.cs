using Microsoft.EntityFrameworkCore;
using ServerDashboardApi.Context;
using System.Net.WebSockets;

namespace ServerDashboardApi.Services
{
    public class DataCleanupService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<DataCleanupService> _logger;

        public DataCleanupService(IServiceScopeFactory scopeFactory, ILogger<DataCleanupService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("DataCleanupService has started in the background.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Start of new cleanup round (checking for old data)...");

                    using var scope = _scopeFactory.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<DashBoardContext>();

                    _logger.LogInformation("Cleanup round completed. Wait until next round (10 min).");
                    await CleanupTemperatures(db);
                    await CleanupEvents(db);

                    await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, "A critical error occurred during the cleanup of the database.");
                    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                }
            }
        }

        private async Task CleanupTemperatures(DashBoardContext db)
        {
            var now = DateTime.Now;

            int daysSinceLastMonday = (int)now.DayOfWeek - (int)DayOfWeek.Monday;

            if(daysSinceLastMonday < 0)
            {
                daysSinceLastMonday += 7;
            }

            var startOfTheWeek = now.Date.AddDays(-daysSinceLastMonday);

            int deletedCount = await db.Tempertures
                .Where(t => t.Date < startOfTheWeek)
                .ExecuteDeleteAsync();

            if (deletedCount > 0)
            {
                _logger.LogInformation($"Old temperatures cleaned up: {deletedCount} measurements from before {startOfTheWeek:dd-MM-yyyy} deleted.");
            }
        }

        private async Task CleanupEvents(DashBoardContext db)
        {
            var now = DateTime.Now;

            int daysSinceLastMonday = (int)now.DayOfWeek - (int)DayOfWeek.Monday;

            if (daysSinceLastMonday < 0)
            {
                daysSinceLastMonday += 7;
            }   

            var startOfTheWeek = now.Date.AddDays(-daysSinceLastMonday);

            int deletedCount = await db.Events
                .Where(e => e.Date < startOfTheWeek)
                .ExecuteDeleteAsync();

            if (deletedCount > 0)
            {
                _logger.LogInformation($"Old events cleaned up: {deletedCount} events from before {startOfTheWeek:dd-MM-yyyy} deleted.");
            }
        }
    }
}
