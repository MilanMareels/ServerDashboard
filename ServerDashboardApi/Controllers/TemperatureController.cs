using Microsoft.AspNetCore.Mvc;
using ServerDashboardApi.Services;

namespace ServerDashboardApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemperatureController(ITemperatureService temperatureService) : ControllerBase
    {
        [HttpGet("Live")]
        public IActionResult GetCurrentTemps()
        {
            return Ok(temperatureService.GetFullDashBoard());
        }

        [HttpGet("Events")]
        public async Task<IActionResult> GetEvents()
        {
            var events = await temperatureService.GetEvents();
            return Ok(events);
        }
    }
}
