using MatchPet.Infrastructure.Repository.Contracts;
using MatchPet.Shared.Errors;

namespace MatchPet.Features.Animal.GetAnimalById;

public class GetAnimalByIdHandler (
  IAnimalRepository animalRepository
) : IBaseHandler<Guid, GetAnimalByIdResponse>
{
  public async Task<GetAnimalByIdResponse> Handle(Guid animalId)
  {
    var animal = await animalRepository.GetById(animalId);

    if (animal is null)
      throw new NotFoundError("Animal não encontrado");
    
    return GetAnimalByIdResponse.FromAnimal(animal);
  }
}