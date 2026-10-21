using ServerDashboardApi.DTOs;
using ServerDashboardApi.Repositories;

namespace ServerDashboardApi.Services
{
    public class TemperatureService(ITemperatureRepo _temperatureRepo) : ITemperatureService
    {
        public DashBoardDTO GetFullDashBoard()
        {
            return _temperatureRepo.GetFullDashBoard();
        }

        public async Task<IEnumerable<EventDTO>> GetEvents()
        {
            return await _temperatureRepo.GetEvents();
        }

        public async Task<IEnumerable<TempertureDTO>> GetHistory()
        {
            return await _temperatureRepo.GetHistory();
        }
    }
}
