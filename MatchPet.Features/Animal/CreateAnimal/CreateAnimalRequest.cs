using MatchPet.Shared.Models;

namespace MatchPet.Features.Animal.CreateAnimal;

public record CreateAnimalRequest(
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
);