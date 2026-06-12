using System.Text.Json.Serialization;

namespace ServerDashboardApi.Models
{
    public class MicroBit
    {
        public int Temp { get; set; }
        public string BackFans { get; set; } = string.Empty;    
        public string TopAndBottomFans { get; set; } = string.Empty;
    }
}
