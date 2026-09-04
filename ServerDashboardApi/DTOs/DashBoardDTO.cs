namespace ServerDashboardApi.DTOs
{
    public class DashBoardDTO
    {
        public int Temp { get; set; }
        public int MaxTemp { get; set; }
        public int MinTemp { get; set; }
        public string TopFans { get; set; } = string.Empty;
        public string BottomFans { get; set; } = string.Empty;
    }
}
