using MatchPet.Infrastructure.Repository.Contracts;
using AnimalEntity = MatchPet.Shared.Models.Animal;
using MatchPet.Infrastructure.Database.Contracts;

namespace MatchPet.Features.Animal.CreateAnimal;

public class CreateAnimalHandler (
  IAnimalRepository animalRepository,
  IUnitOfWork unitOfWork
) : IBaseHandler<CreateAnimalRequest>
{
  public async Task Handle(CreateAnimalRequest request)
  {
    var animal = AnimalEntity.Create(
      request.Name,
      request.Description,
      request.Gender,
      request.Type,
      request.Size,
      request.SupportType,
      request.Photo,
      request.Age,
      request.ShelterSince,
      request.IsVaccinated,
      request.IsSterilized,
      request.IsUnderTreatment,
      request.IsReadyTo,
      request.Breed
    );
    
    animalRepository.Save(animal);
    await unitOfWork.SaveChangesAsync();
  }
}