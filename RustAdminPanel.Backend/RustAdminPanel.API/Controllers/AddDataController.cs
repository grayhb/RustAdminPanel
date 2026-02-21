using Microsoft.AspNetCore.Mvc;
using RustAdminPanel.API.ApiKey;
using RustAdminPanel.Services.ChatMessages;
using RustAdminPanel.Services.PlayerConnections;

namespace RustAdminPanel.API.Controllers
{
    [Route("add-data")]
    [ApiController]
    [ApiKey]
    public class AddDataController : ControllerBase
    {
        private readonly IPlayerConnectionsService _playerConnectionsService;
        private readonly IChatMessageService _chatMessageService;

        public AddDataController(IPlayerConnectionsService playerConnectionsService, IChatMessageService chatMessageService)
        {
            _playerConnectionsService = playerConnectionsService;
            _chatMessageService = chatMessageService;
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

    }
}
