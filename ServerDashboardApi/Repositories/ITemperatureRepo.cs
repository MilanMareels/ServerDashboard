using ServerDashboardApi.DTOs;
using ServerDashboardApi.Models;

namespace ServerDashboardApi.Repositories
{
    public interface ITemperatureRepo
    {
        DashBoardDTO GetFullDashBoard();
        Task<IEnumerable<EventDTO>> GetEvents();
        void GetHistory();
    }
}
