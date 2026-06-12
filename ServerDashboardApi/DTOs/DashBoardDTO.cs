namespace ServerDashboardApi.DTOs
{
    public class DashBoardDTO
    {
        public int Temp { get; set; }
        public int MaxTemp { get; set; }
        public int MinTemp { get; set; }
        public string BackFans { get; set; } = string.Empty;
        public string TopAndBottomFans { get; set; } = string.Empty;
    }
}
