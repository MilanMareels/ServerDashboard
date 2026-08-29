namespace ServerDashboardApi.Models
{
    public class ProxmoxNode
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int RamGb { get; set; }
        public int Cores { get; set; }
        public int StorageGb { get; set; }

        public List<VirtualMachine> VirtualMachines { get; set; } = new();
    }
}
