using Microsoft.AspNetCore.Mvc;
using RustAdminPanel.API.ApiKey;
using RustAdminPanel.Domain.Entities;
using RustAdminPanel.Services.PlayerConnections;
using RustAdminPanel.Services.Profiles;

namespace RustAdminPanel.API.Controllers
{
    [Route("player-profiles")]
    [ApiController]
    [ApiKey]
    public class PlayerProfilesController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly IPlayerConnectionsService _playerConnectionsService;

        public PlayerProfilesController(IProfileService profileService, IPlayerConnectionsService playerConnectionsService)
        {
            _profileService = profileService;
            _playerConnectionsService = playerConnectionsService;
        }

        [HttpPost("list")]
        public async Task<ActionResult<List<PlayerProfile>>> GetPlayerProfiles([FromBody] PlayerProfileQuery playerProfileQuery)
        {
            try
            {
                return await _profileService.GetAsync(playerProfileQuery);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("refresh-data-from-steam")]
        public async Task<ActionResult> RefreshDataFromSteam()
        {
            try
            {
                await _profileService.RefreshSteamDataAsync();

                return Ok("Данные обновлены");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("create-profiles-from-logs")]
        public async Task<ActionResult> CreateProfilesFromLogs()
        {
            try
            {
                await _playerConnectionsService.CreateProfilesFromLogsAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
