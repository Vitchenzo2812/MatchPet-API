using MatchPet.Features.Animal.GetAnimalById;
using MatchPet.Features.Animal.CreateAnimal;
using MatchPet.Features.Animal.UpdateAnimal;
using MatchPet.Features.Animal.GetAnimals;
using MatchPet.Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;
using MatchPet.Features;

namespace MatchPet.Api.Controllers;

[Tags("Animals")]
[Route("animal")]
[ApiController]
public class AnimalController (
  IBaseHandler<GetAnimalsRequest, List<GetAnimalsResponse>> getAnimalsRequestHandler,
  IBaseHandler<Guid, GetAnimalByIdResponse> getAnimalByIdHandler,
  IBaseHandler<CreateAnimalRequest> createAnimalHandler,
  IBaseHandler<UpdateAnimalRequest> updateAnimalHandler,
  IBaseHandler<Guid> deleteAnimalHandler
) : ControllerBase
{
  [HttpGet]
  public async Task<ActionResult<List<GetAnimalsResponse>>> Handle([FromQuery] GetAnimalsRequest request)
  {
    return Ok(await getAnimalsRequestHandler.Handle(request));
  }
  
  [HttpGet("{id:guid}")]
  public async Task<ActionResult<GetAnimalByIdResponse>> HandleGetById(Guid id)
  {
    return Ok(await getAnimalByIdHandler.Handle(id));
  }
  
  [HttpPost]
  [ServiceFilter(typeof(ValidationFilter<CreateAnimalRequest>))]
  public async Task<IActionResult> Handle([FromBody] CreateAnimalRequest request)
  {
    await createAnimalHandler.Handle(request);
    return Ok();
  }

  [HttpPut("{id:guid}")]
  [ServiceFilter(typeof(ValidationFilter<UpdateAnimalRequest>))]
  public async Task<IActionResult> HandleUpdate(Guid id, [FromBody] UpdateAnimalRequest request)
  {
    await updateAnimalHandler.Handle(request with { Id = id });
    return Ok();
  }
  
  [HttpDelete("{id:guid}")]
  public async Task<IActionResult> Handle(Guid id)
  {
    await deleteAnimalHandler.Handle(id);
    return StatusCode(204);
  }
}