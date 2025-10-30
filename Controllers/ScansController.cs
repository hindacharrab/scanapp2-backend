using Microsoft.AspNetCore.Mvc;
using ScanApp2.DTOs;
using ScanApp2.Services.Interfaces;

namespace ScanApp2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScansController : ControllerBase
    {
        private readonly IScanService _scanService;

        public ScansController(IScanService scanService)
        {
            _scanService = scanService;
        }

        [HttpPost]
        public async Task<IActionResult> AddScan([FromBody] ScanInputDto scan)
        {
            var result = await _scanService.ProcessScanAsync(scan);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllScans()
        {
            var scans = await _scanService.GetAllScansAsync();
            return Ok(scans);
        }
    }
}
