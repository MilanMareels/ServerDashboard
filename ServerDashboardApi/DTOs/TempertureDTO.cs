namespace ServerDashboardApi.DTOs
{
    public class TempertureDTO
    {
        public int Temp { get; set; }
        public DateTime Date { get; set; }
        public string TopFans { get; set; } = string.Empty;
        public string BottomFans { get; set; } = string.Empty;
    }
}
