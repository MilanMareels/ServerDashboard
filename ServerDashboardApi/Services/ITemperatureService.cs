using ServerDashboardApi.DTOs;
using ServerDashboardApi.Models;

namespace ServerDashboardApi.Services
{
    public interface ITemperatureService
    {
        DashBoardDTO GetFullDashBoard();
        Task<IEnumerable<EventDTO>> GetEvents();
        Task<IEnumerable<TempertureDTO>> GetHistory();
    }
}
