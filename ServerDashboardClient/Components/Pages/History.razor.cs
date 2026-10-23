using ChartJs.Blazor.Common;
using ChartJs.Blazor.Common.Enums;
using ChartJs.Blazor.LineChart;
using Microsoft.AspNetCore.Components;
using ServerDashboardApi.DTOs;
using ServerDashboardClient.Services;

namespace ServerDashboardClient.Components.Pages
{
    public partial class History
    {
        [Inject]
        public ITemperatureService _temperatureService { get; set; }

        private LineConfig _config;
        public IEnumerable<TempertureDTO>? data { get; set; }

        protected override async Task OnInitializedAsync()
        {
            data = await _temperatureService.GetHistory();
            BuildLineChart();
        }

        public void BuildLineChart()
        {
            _config = new LineConfig
            {
                Options = new LineOptions
                {
                    Responsive = true,
                    Title = new OptionsTitle
                    {
                        Display = true,
                        Text = "Temperatures history"
                    },
                    Tooltips = new Tooltips
                    {
                        Mode = InteractionMode.Point,
                        Intersect = true
                    },
                    Hover = new Hover
                    {
                        Mode = InteractionMode.Index,
                        Intersect = true
                    }
                }
            };

            foreach (var item in data)
            {
                _config.Data.Labels.Add(item.Date.ToString("dd/MM HH:mm"));
            }

            IDataset<int> dataset = new LineDataset<int>(data.Select(x => x.Temp))
            {
                Label = "Temperature",
                BackgroundColor = "#4f46e5",
                BorderColor = "#4f46e5",
                Fill = FillingMode.Disabled,
            };

            _config.Data.Datasets.Add(dataset);
        }
    }
}