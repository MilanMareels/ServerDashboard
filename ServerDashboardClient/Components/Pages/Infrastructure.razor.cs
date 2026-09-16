using Microsoft.AspNetCore.Components;
using ServerDashboardApi.DTOs;
using ServerDashboardClient.Components.Models;
using ServerDashboardClient.Services;

namespace ServerDashboardClient.Components.Pages
{
    public partial class Infrastructure
    {
        [Inject]
        public IInfrastructureService _infrastructureService { get; set; }
        private List<ProxmoxNodeDTO>? nodes;
        private VirtualMachineDTO currentVm = new();
        private ProxmoxNodeDTO currentNode = new();
        private bool isEditing = false;

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            nodes = await _infrastructureService.GetNodesWithVMsAsync();
        }

        private async Task SaveNode()
        {
            if (string.IsNullOrWhiteSpace(currentNode.Name)) return;

            await _infrastructureService.AddNodeAsync(currentNode);
            currentNode = new ProxmoxNodeDTO();
            await LoadData();
        }

        private async Task SaveVm()
        {
            if (currentVm.ProxmoxNodeId == 0) return;
            if (isEditing)
            {
                await _infrastructureService.UpdateVirtualMachineAsync(currentVm);
            }
            else
            {
                await _infrastructureService.AddVirtualMachineAsync(currentVm);
            }

            ResetForm();
            await LoadData();
        }

        private void EditVm(VirtualMachineDTO vm)
        {
            currentVm = new VirtualMachineDTO
            {
                Id = vm.Id,
                Name = vm.Name,
                RamGb = vm.RamGb,
                Cores = vm.Cores,
                StorageGb = vm.StorageGb,
                Notes = vm.Notes,
                ProxmoxNodeId = vm.ProxmoxNodeId
            };
            isEditing = true;
        }

        private async Task DeleteVm(int id)
        {
            await _infrastructureService.DeleteVirtualMachineAsync(id);
            await LoadData();
        }

        private Usage GetUsage(int id)
        {
            var node = nodes.FirstOrDefault(n => n.Id == id).VirtualMachines;

            var ram = node.Sum(vm => vm.RamGb);
            var cores = node.Sum(vm => vm.Cores);
            var storage = node.Sum(vm => vm.StorageGb);

            return new Usage
            {
                RamGb = ram,
                Cores = cores,
                StorageGb = storage
            };
        }

        private void ResetForm()
        {
            currentVm = new VirtualMachineDTO();
            isEditing = false;
        }
    }
}
