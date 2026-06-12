using ServerDashboardApi.DTOs;

namespace ServerDashboardClient.Services
{
    public interface ITemperatureService
    {
        Task<DashBoardDTO> GetFullDashBoard();
        Task<IEnumerable<EventDTO>> GetEvents();
    }
}
