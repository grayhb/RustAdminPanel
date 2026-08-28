using Microsoft.AspNetCore.Mvc;
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

        //[HttpPost("report")]
        //public async Task<ActionResult> AddPlayerReport([FromBody] PlayerReportDto dto)
        //{
        //    try
        //    {
        //        await _playerReportsService.AddAsync(dto);

        //        return Ok("Player report saved");
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.InnerException != null)
        //        {
        //            return BadRequest($"Inner Exception: {ex.InnerException.Message}");
        //        }

        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpPost("server-report")]
        public async Task<ActionResult> AddServerReport(
            [FromForm] string data,
            [FromForm] string userid
        )
        {
            try
            {
                await _playerReportsService.AddFromServerAsync(new ReportDto() { data = data, userid = userid});

                return Ok("Report saved");
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
