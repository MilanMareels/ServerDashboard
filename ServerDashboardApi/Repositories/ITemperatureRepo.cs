using ServerDashboardApi.DTOs;

namespace ServerDashboardApi.Repositories
{
    public interface ITemperatureRepo
    {
        DashBoardDTO GetFullDashBoard();
        Task<IEnumerable<EventDTO>> GetEvents();
        Task<IEnumerable<TempertureDTO>> GetHistory();
    }
}
