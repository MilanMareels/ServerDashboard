using Microsoft.AspNetCore.Components;
using ServerDashboardApi.DTOs;
using ServerDashboardClient.Services;

namespace ServerDashboardClient.Components.Pages
{
    public partial class Home : IDisposable
    {
        [Inject]
        public ITemperatureService _temperatureService { get; set; }
        public DashBoardDTO? dashBoard { get; set; }
        public IEnumerable<EventDTO>? serverEvents { get; set; }

        private PeriodicTimer? _timer;
        private CancellationTokenSource _cts = new();

        protected override async Task OnInitializedAsync()
        {
            dashBoard = await _temperatureService.GetFullDashBoard();
            serverEvents = await _temperatureService.GetEvents();

            _ = StartAutoRefreshAsync();
        }

        // Refresh every 1 sec, get api data every 1 sec.
        private async Task StartAutoRefreshAsync()
        {
            _timer = new PeriodicTimer(TimeSpan.FromSeconds(1));

            try
            {
                while (await _timer.WaitForNextTickAsync(_cts.Token))
                {
                    dashBoard = await _temperatureService.GetFullDashBoard();
                    serverEvents = await _temperatureService.GetEvents();

                    await InvokeAsync(StateHasChanged);
                }
            }
            catch (OperationCanceledException)
            {
            }
        }

        // When Page closes Timer cancels. Clean up.
        public void Dispose()
        {
            _cts.Cancel();
            _timer?.Dispose();
            _cts.Dispose();
        }

        // ---- Presentation helpers for the UI ----
        public bool IsHighTemp => dashBoard != null && dashBoard.Temp >= 25;
        public string TempStatus => IsHighTemp ? "High" : "Normal";
        public string TempValueClass => IsHighTemp ? "dash-temp--high" : "dash-temp--normal";
        public string TempPillClass => IsHighTemp ? "dash-pill--danger" : "dash-pill--success";

        public int TempPercent
        {
            get
            {
                if (dashBoard == null || dashBoard.MaxTemp <= dashBoard.MinTemp)
                {
                    return 50;
                }

                return Math.Clamp(
                    (int)Math.Round((dashBoard.Temp - dashBoard.MinTemp) / (double)(dashBoard.MaxTemp - dashBoard.MinTemp) * 100),
                    0,
                    100);
            }
        }
    }
}