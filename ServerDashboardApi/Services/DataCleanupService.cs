using Microsoft.EntityFrameworkCore;
using ServerDashboardApi.Context;

namespace ServerDashboardApi.Services
{
    public class DataCleanupService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DataCleanupService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<DashBoardContext>();

                    await CleanupTemperatures(db);
                    await CleanupEvents(db);

                    await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
                }
                catch { }
            }
        }

        private async Task CleanupTemperatures(DashBoardContext db)
        {
            var cutoff = DateTime.UtcNow.AddDays(-7);

            await db.Tempertures
                .Where(t => t.Date < cutoff)
                .ExecuteDeleteAsync();
        }

        private async Task CleanupEvents(DashBoardContext db)
        {
            var cutoff = DateTime.UtcNow.AddDays(-5);

            await db.Events
                .Where(e => e.Date < cutoff)
                .ExecuteDeleteAsync();

            var extra = await db.Events
                .OrderByDescending(e => e.Date)
                .Skip(30)
                .ToListAsync();

            if (extra.Any())
            {
                db.Events.RemoveRange(extra);
                await db.SaveChangesAsync();
            }
        }
    }
}
