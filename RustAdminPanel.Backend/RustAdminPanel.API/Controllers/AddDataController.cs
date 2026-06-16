using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RustAdminPanel.API.ApiKey;
using RustAdminPanel.Services.ChatMessages;
using RustAdminPanel.Services.PlayerConnections;
using RustAdminPanel.Services.PlayerReports;

namespace RustAdminPanel.API.Controllers
{
    [Route("add-data")]
    [ApiController]
    public class AddDataController : ControllerBase
    {
        private readonly IPlayerConnectionsService _playerConnectionsService;
        private readonly IChatMessageService _chatMessageService;
        private readonly IPlayerReportsService _playerReportsService;

        public AddDataController(IPlayerConnectionsService playerConnectionsService, IChatMessageService chatMessageService, IPlayerReportsService playerReportsService)
        {
            _playerConnectionsService = playerConnectionsService;
            _chatMessageService = chatMessageService;
            _playerReportsService = playerReportsService;
        }

        [HttpPost("player-connection")]
        public async Task<ActionResult> AddPlayerConnection([FromBody] PlayerConnectionDto dto)
        {
            try
            {
                await _playerConnectionsService.AddAsync(dto);

                return Ok("Player connection saved");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return BadRequest($"Inner Exception: {ex.InnerException.Message}");
                }

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("chat-message")]
        public async Task<ActionResult> AddChatMessage([FromBody] ChatMessageDto dto)
        {
            try
            {
                await _chatMessageService.AddAsync(dto);

                return Ok("Message saved");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return BadRequest($"Inner Exception: {ex.InnerException.Message}");
                }

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("player-report"), AllowAnonymous]
        public async Task<ActionResult> AddPlayerReport([FromBody] PlayerReportDto dto)
        {
            try
            {
                await _playerReportsService.AddAsync(dto);

                return Ok("Player report saved");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return BadRequest($"Inner Exception: {ex.InnerException.Message}");
                }

                return BadRequest(ex.Message);
            }
        }
    }
}
