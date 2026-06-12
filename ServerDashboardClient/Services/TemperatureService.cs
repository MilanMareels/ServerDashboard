using ServerDashboardApi.DTOs;

namespace ServerDashboardClient.Services
{
    public class TemperatureService: ITemperatureService
    {
        private readonly HttpClient _httpClient;
        public TemperatureService(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

        public async Task<DashBoardDTO> GetFullDashBoard()
        {
            return await _httpClient.GetFromJsonAsync<DashBoardDTO>("Temperature/Live");
        }

        public async Task<IEnumerable<EventDTO>> GetEvents()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<EventDTO>>("Temperature/Events");
        }
    }
}
