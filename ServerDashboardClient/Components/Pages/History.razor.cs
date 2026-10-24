using ChartJs.Blazor.Common;
using ChartJs.Blazor.Common.Axes;
using ChartJs.Blazor.Common.Axes.Ticks;
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
                        Text = "Server Rack Temperature History"
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
                    },

                    Scales = new Scales
                    {
                        XAxes = new List<CartesianAxis>
                        {
                            new CategoryAxis
                            {
                                ScaleLabel = new ScaleLabel
                                {
                                    Display = true,
                                    LabelString = "Date and Time"
                                }
                            }
                        },
                        YAxes = new List<CartesianAxis>
                        {
                            new LinearCartesianAxis
                            {
                                ScaleLabel = new ScaleLabel
                                {
                                    Display = true,
                                    LabelString = "Temperature (°C)"
                                },
                                Ticks = new LinearCartesianTicks
                                {
                                    BeginAtZero = false
                                }
                            }
                        }
                    }
                }
            };

            foreach (var item in data)
            {
                _config.Data.Labels.Add(item.Date.ToString("dd/MM HH:mm"));
            }

            IDataset<int> dataset = new LineDataset<int>(data.Select(x => x.Temp))
            {
                Label = "Temperature (°C)",
                BackgroundColor = "#4f46e5",
                BorderColor = "#4f46e5",
                BorderWidth = 2,
                Fill = FillingMode.Disabled,
            };

            _config.Data.Datasets.Add(dataset);
        }
    }
}