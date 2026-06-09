using MatchPet.Shared.Models;
using AnimalEntity = MatchPet.Shared.Models.Animal;

namespace MatchPet.Features.Animal.GetAnimalById;

public record GetAnimalByIdResponse(
  Guid Id,
  string Name,
  string Description,
  AnimalGender Gender,
  AnimalType Type,
  AnimalSize Size,
  AnimalSupportType SupportType,
  string Photo,
  int Age,
  DateTime ShelterSince,
  bool IsVaccinated,
  bool IsSterilized,
  bool IsUnderTreatment,
  bool IsReadyTo,
  string? Breed
)
{
  public static GetAnimalByIdResponse FromAnimal(AnimalEntity animal)
  {
    return new GetAnimalByIdResponse(
      animal.Id,
      animal.Name,
      animal.Description,
      animal.Gender,
      animal.Type,
      animal.Size,
      animal.SupportType,
      animal.Photo,
      animal.Age,
      animal.ShelterSince,
      animal.IsVaccinated,
      animal.IsSterilized,
      animal.IsUnderTreatment,
      animal.IsReadyTo,
      animal.Breed
    );
  }
}