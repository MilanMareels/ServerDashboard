namespace ServerDashboardApi.DTOs
{
    public class EventDTO
    {
        public DateTime Date { get; set; }
        public int Temp { get; set; }
        public string? Severity { get; set; }
    }
}
