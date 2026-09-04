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

            if (data == null)
            {
                return new DashBoardDTO
                {
                    Temp = 0,
                    MaxTemp = 0,
                    MinTemp = 0,
                    TopFans = "OFF",
                    BottomFans = "OFF"
                };
            }

            return new DashBoardDTO
            {
                Temp = data.Temp,
                MaxTemp = data.MaxTemp,
                MinTemp = data.MinTemp,
                TopFans = data.TopFans,
                BottomFans = data.BottomFans,
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
