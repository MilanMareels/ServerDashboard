namespace ServerDashboardApi.Models
{
    public class Temperture
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Temp { get; set; }
        public string TopFans { get; set; } = string.Empty;
        public string BottomFans { get; set; } = string.Empty;
    }
}