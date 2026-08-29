using ServerDashboardApi.DTOs;
using ServerDashboardApi.Models;

namespace ServerDashboardApi.Services
{
    public interface IInfrastructureService
    {
        Task<List<ProxmoxNodeDTO>> GetNodesWithVMsAsync();
        Task AddVirtualMachineAsync(VirtualMachineDTO vmDto);
        Task UpdateVirtualMachineAsync(VirtualMachineDTO vmDto);
        Task DeleteVirtualMachineAsync(int id);
        Task AddNodeAsync(ProxmoxNodeDTO nodeDto);
    }
}
