using ServerDashboardApi.DTOs;

namespace ServerDashboardClient.Services
{
    public class InfrastructureClientService(HttpClient _httpClient) : IInfrastructureClientService
    {
        public async Task<List<ProxmoxNodeDTO>?> GetNodesWithVMsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ProxmoxNodeDTO>>("infrastructure/nodes");
        }

        public async Task AddVirtualMachineAsync(VirtualMachineDTO vmDto)
        {
            await _httpClient.PostAsJsonAsync("infrastructure/vm", vmDto);
        }

        public async Task UpdateVirtualMachineAsync(VirtualMachineDTO vmDto)
        {
            await _httpClient.PutAsJsonAsync("infrastructure/vm", vmDto);
        }

        public async Task DeleteVirtualMachineAsync(int id)
        {
            await _httpClient.DeleteAsync($"infrastructure/vm/{id}");
        }

        public async Task AddNodeAsync(ProxmoxNodeDTO nodeDto)
        {
            await _httpClient.PostAsJsonAsync("infrastructure/node", nodeDto);
        }
    }
}
