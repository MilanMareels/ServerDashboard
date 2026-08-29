using Microsoft.EntityFrameworkCore;
using ServerDashboardApi.Context;
using ServerDashboardApi.DTOs;
using ServerDashboardApi.Models;
using ServerDashboardApi.Repositories;

namespace ServerDashboardApi.Services
{
    public class InfrastructureService(IInfrastructureRepo _repo) : IInfrastructureService
    {
        public async Task<List<ProxmoxNodeDTO>> GetNodesWithVMsAsync()
        {
            var nodes = await _repo.GetNodesWithVMsAsync();

            // Map Models naar DTOs
            return nodes.Select(n => new ProxmoxNodeDTO
            {
                Id = n.Id,
                Name = n.Name,
                RamGb = n.RamGb,
                Cores = n.Cores,
                StorageGb = n.StorageGb,
                VirtualMachines = n.VirtualMachines.Select(vm => new VirtualMachineDTO
                {
                    Id = vm.Id,
                    Name = vm.Name,
                    RamGb = vm.RamGb,
                    Cores = vm.Cores,
                    StorageGb = vm.StorageGb,
                    Notes = vm.Notes,
                    ProxmoxNodeId = vm.ProxmoxNodeId
                }).ToList()
            }).ToList();
        }

        public async Task AddVirtualMachineAsync(VirtualMachineDTO vmDto)
        {
            var vm = new VirtualMachine
            {
                Name = vmDto.Name,
                RamGb = vmDto.RamGb,
                Cores = vmDto.Cores,
                StorageGb = vmDto.StorageGb,
                Notes = vmDto.Notes,
                ProxmoxNodeId = vmDto.ProxmoxNodeId
            };
            await _repo.AddVirtualMachineAsync(vm);
        }

        public async Task UpdateVirtualMachineAsync(VirtualMachineDTO vmDto)
        {
            var vm = new VirtualMachine
            {
                Id = vmDto.Id,
                Name = vmDto.Name,
                RamGb = vmDto.RamGb,
                Cores = vmDto.Cores,
                StorageGb = vmDto.StorageGb,
                Notes = vmDto.Notes,
                ProxmoxNodeId = vmDto.ProxmoxNodeId
            };
            await _repo.UpdateVirtualMachineAsync(vm);
        }

        public async Task DeleteVirtualMachineAsync(int id)
        {
            await _repo.DeleteVirtualMachineAsync(id);
        }

        public async Task AddNodeAsync(ProxmoxNodeDTO nodeDto)
        {
            var node = new ProxmoxNode
            {
                Name = nodeDto.Name,
                RamGb = nodeDto.RamGb,
                Cores = nodeDto.Cores,
                StorageGb = nodeDto.StorageGb
            };

            await _repo.AddNodeAsync(node);
        }
    }
}
