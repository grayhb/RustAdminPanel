using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RustAdminPanel.Domain.Entities;
using RustAdminPanel.Services.ChatMessages;
using RustAdminPanel.Services.PlayerConnections;

namespace RustAdminPanel.API.Controllers
{
    [Route("get-data")]
    [ApiController]
    public class GetDataController : ControllerBase
    {
        private readonly IPlayerConnectionsService _playerConnectionsService;
        private readonly IChatMessageService _chatMessageService;

        public GetDataController(IPlayerConnectionsService playerConnectionsService, IChatMessageService chatMessageService)
        {
            _playerConnectionsService = playerConnectionsService;
            _chatMessageService = chatMessageService;
        }

        [HttpGet]
        public async Task<List<ChatMessage>> Index()
        {
            return await _chatMessageService.GetAsync(new ChatMessageQuery());
        }

        [HttpPost("player-connection")]
        public async Task<ActionResult<List<PlayerConnectionLog>>> GetPlayerConnections([FromBody] PlayerConnectionQuery query)
        {
            try
            {
                return await _playerConnectionsService.GetAsync(query);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("chat-messages")]
        public async Task<ActionResult<List<ChatMessage>>> GetChatMessages([FromBody] ChatMessageQuery query)
        {
            try
            {
                return await _chatMessageService.GetAsync(query);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
