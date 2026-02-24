using Microsoft.AspNetCore.Mvc;
using RustAdminPanel.API.ApiKey;

namespace RustAdminPanel.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ApiKey]
    public class AuthController : ControllerBase
    {
        [HttpGet("check")]
        public ActionResult CheckApiKey()
        {
            return Ok();
        }
    }
}
