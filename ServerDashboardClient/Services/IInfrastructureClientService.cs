using ServerDashboardApi.DTOs;

namespace ServerDashboardClient.Services
{
    public interface IInfrastructureClientService
    {
        Task<List<ProxmoxNodeDTO>?> GetNodesWithVMsAsync();
        Task AddVirtualMachineAsync(VirtualMachineDTO vmDto);
        Task UpdateVirtualMachineAsync(VirtualMachineDTO vmDto);
        Task DeleteVirtualMachineAsync(int id);
        Task AddNodeAsync(ProxmoxNodeDTO nodeDto);
    }
}
