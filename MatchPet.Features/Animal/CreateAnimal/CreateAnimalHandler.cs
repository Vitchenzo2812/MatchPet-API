using MatchPet.Infrastructure.Repository.Contracts;
using AnimalEntity = MatchPet.Shared.Models.Animal;
using MatchPet.Infrastructure.Database.Contracts;
using MatchPet.Infrastructure.Services.Contracts;

namespace MatchPet.Features.Animal.CreateAnimal;

public class CreateAnimalHandler (
  IAnimalRepository animalRepository,
  IFileManagerService fileManagerService,
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
      request.Age,
      request.ShelterSince,
      request.IsVaccinated,
      request.IsSterilized,
      request.IsUnderTreatment,
      request.IsReadyTo,
      request.Breed
    );

    var photo = await fileManagerService.SaveFile(request.Photo, animal.Id.ToString());

    animal.UpdatePhoto(photo);
    
    animalRepository.Save(animal);
    await unitOfWork.SaveChangesAsync();
  }
}