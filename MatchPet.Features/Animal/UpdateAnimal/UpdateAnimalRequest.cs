using MatchPet.Shared.Models;

namespace MatchPet.Features.Animal.UpdateAnimal;

public record UpdateAnimalRequest(
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
  public Guid Id { get; init; }
}