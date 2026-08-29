namespace ServerDashboardApi.DTOs
{
    public class VirtualMachineDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int RamGb { get; set; }
        public int Cores { get; set; }
        public double StorageGb { get; set; }
        public string Notes { get; set; } = string.Empty;
        public int ProxmoxNodeId { get; set; }
    }
}
