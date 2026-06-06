using MatchPet.Features.Animal.CreateAnimal;
using MatchPet.Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;
using MatchPet.Features;

namespace MatchPet.Api.Controllers;

[Tags("Animals")]
[Route("animal")]
[ApiController]
public class AnimalController (
  IBaseHandler<CreateAnimalRequest> createAnimalHandler
) : ControllerBase
{
  [HttpPost]
  [ServiceFilter(typeof(ValidationFilter<CreateAnimalRequest>))]
  public async Task<IActionResult> Handle([FromBody] CreateAnimalRequest request)
  {
    await createAnimalHandler.Handle(request);
    return Ok();
  }
}