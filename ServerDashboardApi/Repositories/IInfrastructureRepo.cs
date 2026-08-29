using ServerDashboardApi.Models;

namespace ServerDashboardApi.Repositories
{
    public interface IInfrastructureRepo
    {
        Task<List<ProxmoxNode>> GetNodesWithVMsAsync();
        Task AddVirtualMachineAsync(VirtualMachine vm);
        Task UpdateVirtualMachineAsync(VirtualMachine vm);
        Task DeleteVirtualMachineAsync(int id);
        Task AddNodeAsync(ProxmoxNode node);
    }
}
