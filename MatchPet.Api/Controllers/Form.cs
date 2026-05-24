using Microsoft.AspNetCore.Mvc;

namespace MatchPet.Api.Controllers;

[Tags("Forms")]
[Route("forms")]
[ApiController]
public class Form : ControllerBase
{
  [HttpPost("/adopt")]
  public async Task<IActionResult> Handle()
  {
    return Ok();
  }
}