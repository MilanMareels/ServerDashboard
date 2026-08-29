using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerDashboardApi.DTOs;
using ServerDashboardApi.Services;

namespace ServerDashboardApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfrastructureController(IInfrastructureService _service) : ControllerBase
    {
        [HttpGet("nodes")]
        public async Task<ActionResult<List<ProxmoxNodeDTO>>> GetNodesWithVMs()
        {
            var result = await _service.GetNodesWithVMsAsync();
            return Ok(result);
        }

        [HttpPost("vm")]
        public async Task<IActionResult> AddVirtualMachine([FromBody] VirtualMachineDTO vmDto)
        {
            await _service.AddVirtualMachineAsync(vmDto);
            return Ok();
        }

        [HttpPut("vm")]
        public async Task<IActionResult> UpdateVirtualMachine([FromBody] VirtualMachineDTO vmDto)
        {
            await _service.UpdateVirtualMachineAsync(vmDto);
            return Ok();
        }

        [HttpDelete("vm/{id}")]
        public async Task<IActionResult> DeleteVirtualMachine(int id)
        {
            await _service.DeleteVirtualMachineAsync(id);
            return Ok();
        }

        [HttpPost("node")]
        public async Task<IActionResult> AddNode([FromBody] ProxmoxNodeDTO nodeDto)
        {
            await _service.AddNodeAsync(nodeDto);
            return Ok();
        }
    }
}
