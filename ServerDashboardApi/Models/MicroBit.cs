using System.Text.Json.Serialization;

namespace ServerDashboardApi.Models
{
    public class MicroBit
    {
        public int Temp { get; set; }
        public string TopFans { get; set; } = string.Empty;
        public string BottomFans { get; set; } = string.Empty;
    }
}
