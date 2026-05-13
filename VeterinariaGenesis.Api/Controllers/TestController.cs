using Microsoft.AspNetCore.Mvc;

namespace VeterinariaGenesis.Api.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("¡El servidor está vivo y respondiendo!");
    }
}
