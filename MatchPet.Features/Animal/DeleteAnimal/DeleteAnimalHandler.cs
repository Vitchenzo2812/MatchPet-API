using MatchPet.Infrastructure.Repository.Contracts;
using MatchPet.Infrastructure.Database.Contracts;
using MatchPet.Shared.Errors;

namespace MatchPet.Features.Animal.DeleteAnimal;

public class DeleteAnimalHandler (
  IAnimalRepository animalRepository,
  IUnitOfWork unitOfWork
) : IBaseHandler<Guid>
{
  public async Task Handle(Guid animalId)
  {
    var animal = await animalRepository.GetById(animalId);

    if (animal is null)
      throw new NotFoundError("Animal não encontrado");
    
    animalRepository.Delete(animal);
    await unitOfWork.SaveChangesAsync();
  }
}