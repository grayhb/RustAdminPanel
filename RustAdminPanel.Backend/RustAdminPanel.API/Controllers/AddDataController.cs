using Microsoft.AspNetCore.Mvc;
using RustAdminPanel.API.ApiKey;
using RustAdminPanel.Services.PlayerConnections;

namespace RustAdminPanel.API.Controllers
{
    [Route("api/add-data")]
    [ApiController]
    [ApiKey]
    public class AddDataController : ControllerBase
    {
        private readonly IPlayerConnectionsService _playerConnectionsService;

        public AddDataController(IPlayerConnectionsService playerConnectionsService)
        {
            _playerConnectionsService = playerConnectionsService;
        }

        [HttpPost("player-connection")]
        public async Task<ActionResult> AddPlayerConnection([FromBody] PlayerConnectionDto dto)
        {
            try
            {
                await _playerConnectionsService.AddAsync(dto);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
