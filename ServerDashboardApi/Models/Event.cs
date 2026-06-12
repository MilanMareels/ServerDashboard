namespace ServerDashboardApi.Models
{
    public class Event
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Temp { get; set; }
        public string? Severity { get; set; }
    }
}
