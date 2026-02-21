using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RustAdminPanel.Domain.Entities;
using RustAdminPanel.Services.PlayerConnections;

namespace RustAdminPanel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetDataController : ControllerBase
    {
        private readonly IPlayerConnectionsService _playerConnectionsService;

        public GetDataController(IPlayerConnectionsService playerConnectionsService)
        {
            _playerConnectionsService = playerConnectionsService;
        }

        [HttpGet("player-connection")]
        public async Task<ActionResult<List<PlayerConnectionLog>>> GetPlayerConnections()
        {
            try
            {
                return await _playerConnectionsService.GetAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
