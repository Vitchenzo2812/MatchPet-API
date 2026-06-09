using MatchPet.Infrastructure.Repository.Contracts;
using MatchPet.Infrastructure.Database.Contracts;
using MatchPet.Shared.Errors;

namespace MatchPet.Features.Animal.UpdateAnimal;

public class UpdateAnimalHandler (
  IAnimalRepository animalRepository,
  IUnitOfWork unitOfWork
) : IBaseHandler<UpdateAnimalRequest>
{
  public async Task Handle(UpdateAnimalRequest request)
  {
    var animal = await animalRepository.GetById(request.Id);

    if (animal is null)
      throw new NotFoundError("Animal não encontrado");

    animal
      .UpdateName(request.Name)
      .UpdateDescription(request.Description)
      .UpdateGender(request.Gender)
      .UpdateType(request.Type)
      .UpdateSize(request.Size)
      .UpdateSupportType(request.SupportType)
      .UpdatePhoto(request.Photo)
      .UpdateAge(request.Age)
      .UpdateShelterSince(request.ShelterSince)
      .UpdateIsVaccinated(request.IsVaccinated)
      .UpdateIsSterilized(request.IsSterilized)
      .UpdateIsUnderTreatment(request.IsUnderTreatment)
      .UpdateIsReadyTo(request.IsReadyTo)
      .UpdateBreed(request.Breed);
    
    await unitOfWork.SaveChangesAsync();
  }
}