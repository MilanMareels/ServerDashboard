using Microsoft.EntityFrameworkCore;
using ServerDashboardApi.Models;

namespace ServerDashboardApi.Context
{
    public class DashBoardContext : DbContext
    {
        public DashBoardContext(DbContextOptions<DashBoardContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Temperture> Tempertures { get; set; }
        public DbSet<Event> Events { get; set; }
    }
}