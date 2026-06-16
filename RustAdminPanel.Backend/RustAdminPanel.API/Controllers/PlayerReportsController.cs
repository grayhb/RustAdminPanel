using Microsoft.AspNetCore.Mvc;
using RustAdminPanel.API.ApiKey;
using RustAdminPanel.Domain.Entities;
using RustAdminPanel.Services.PlayerReports;

namespace RustAdminPanel.API.Controllers
{
    [Route("player-reports")]
    [ApiController]
    [ApiKey]
    public class PlayerReportsController : ControllerBase
    {
        private readonly IPlayerReportsService _playerReportsService;

        public PlayerReportsController(IPlayerReportsService playerReportsService)
        {
            _playerReportsService = playerReportsService;
        }

        [HttpPost("list")]
        public async Task<ActionResult<List<PlayerReport>>> GetPlayerProfiles([FromBody] PlayerReportQuery playerReportQuery)
        {
            try
            {
                return await _playerReportsService.GetAsync(playerReportQuery);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
