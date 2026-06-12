using Microsoft.EntityFrameworkCore;
using ServerDashboardApi.Context;
using ServerDashboardApi.DTOs;
using ServerDashboardApi.Models;
using ServerDashboardApi.Services;
using System.Collections;

namespace ServerDashboardApi.Repositories
{
    public class TemperatureRepo(DashBoardContext _context, LiveTemperatureCaching _caching) : ITemperatureRepo
    {
        public DashBoardDTO GetFullDashBoard()
        {
            var data = _caching.GetCurrentTemperature();

            return new DashBoardDTO
            {
                Temp = data.Temp,
                MaxTemp = data.MaxTemp,
                MinTemp = data.MinTemp,
                BackFans = data.BackFans,
                TopAndBottomFans = data.TopAndBottomFans,
            };
        }

        public async Task<IEnumerable<EventDTO>> GetEvents()
        {
            return await _context.Events.
                OrderByDescending(e => e.Date).
                Take(10)
                .Select(e => new EventDTO()
                {
                    Date = e.Date,
                    Temp = e.Temp,
                    Severity = e.Severity,
                })
                .ToListAsync();
        }

        public void GetHistory()
        {
            throw new NotImplementedException();
        }
    }
}
