using AnimalEntity = MatchPet.Shared.Models.Animal;
using MatchPet.Shared.Models;

namespace MatchPet.Features.Animal.GetAnimals;

public record GetAnimalsResponse(
  Guid Id,
  string Name,
  string Photo,
  int Age,
  DateTime ShelterSince,
  AnimalGender Gender,
  AnimalType Type,
  AnimalSize Size,
  bool IsVaccinated,
  bool IsReadyTo,
  string? Breed
)
{
  public static GetAnimalsResponse FromEntity(AnimalEntity animal)
  {
    return new GetAnimalsResponse(
      Id: animal.Id,
      Name: animal.Name,
      Photo: animal.Photo,
      Age: animal.Age,
      ShelterSince: animal.ShelterSince,
      Gender: animal.Gender,
      Type: animal.Type,
      Size: animal.Size,
      IsVaccinated: animal.IsVaccinated,
      IsReadyTo: animal.IsReadyTo,
      Breed: animal.Breed
    );
  }
}