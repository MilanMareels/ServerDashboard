namespace ServerDashboardApi.Models
{
    public class CachedSensorMetrics
    {
        public int Temp { get; set; }
        public int MaxTemp { get; set; }
        public int MinTemp { get; set; }
        public string TopFans { get; set; } = string.Empty;
        public string BottomFans { get; set; } = string.Empty;
         
    }
}
