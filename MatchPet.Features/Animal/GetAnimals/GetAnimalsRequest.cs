using MatchPet.Shared.Models;

namespace MatchPet.Features.Animal.GetAnimals;

public record GetAnimalsRequest(
  AnimalType? Type,
  AnimalGender? Gender,
  AnimalSupportType? SupportType,
  bool? IsVaccinated,
  bool? IsSterilized,
  bool? IsUnderTreatment,
  bool? IsReadyTo,
  AnimalAgeRange? AgeRange
);