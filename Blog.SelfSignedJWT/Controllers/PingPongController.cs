using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.SelfSignedJWT.Controllers;

[ApiController]
[Route("pingpong")]
[Authorize]
public class PingPongController : ControllerBase
{
    [HttpGet("ping")]
    public IActionResult Ping()
    {
        return Content("Pong");
    }
}