using ServerDashboardApi.Context;
using Microsoft.EntityFrameworkCore;
using ServerDashboardApi.Models;

namespace ServerDashboardApi.Repositories
{
    public class InfrastructureRepo(DashBoardContext _context) : IInfrastructureRepo
    {
        public async Task<List<ProxmoxNode>> GetNodesWithVMsAsync()
        {
            return await _context.ProxmoxNodes
                .Include(n => n.VirtualMachines)
                .ToListAsync();
        }

        public async Task AddVirtualMachineAsync(VirtualMachine vm)
        {
            _context.VirtualMachines.Add(vm);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateVirtualMachineAsync(VirtualMachine vm)
        {
            _context.VirtualMachines.Update(vm);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVirtualMachineAsync(int id)
        {
            var vm = await _context.VirtualMachines.FindAsync(id);
            if (vm != null)
            {
                _context.VirtualMachines.Remove(vm);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddNodeAsync(ProxmoxNode node)
        {
            _context.ProxmoxNodes.Add(node);
            await _context.SaveChangesAsync();
        }
    }
}
