using ServerDashboardApi.DTOs;

namespace ServerDashboardClient.Components.Pages
{
    public partial class Infrastructure
    {
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
            nodes = await _infraClient.GetNodesWithVMsAsync();
        }

        private async Task SaveNode()
        {
            if (string.IsNullOrWhiteSpace(currentNode.Name)) return;

            await _infraClient.AddNodeAsync(currentNode);
            currentNode = new ProxmoxNodeDTO();
            await LoadData();
        }

        private async Task SaveVm()
        {
            if (currentVm.ProxmoxNodeId == 0) return;
            if (isEditing)
            {
                await _infraClient.UpdateVirtualMachineAsync(currentVm);
            }
            else
            {
                await _infraClient.AddVirtualMachineAsync(currentVm);
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
            await _infraClient.DeleteVirtualMachineAsync(id);
            await LoadData();
        }

        private void ResetForm()
        {
            currentVm = new VirtualMachineDTO();
            isEditing = false;
        }
    }
}