namespace ServerDashboardApi.Models
{
    public class Temperture
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Temp { get; set; }
        public string BackFans { get; set; } = string.Empty;
        public string TopAndBottomFans { get; set; } = string.Empty;
    }
}