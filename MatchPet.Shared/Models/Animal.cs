using MatchPet.Shared.Extensions;
using MatchPet.Shared.Errors;

namespace MatchPet.Shared.Models;

public class Animal : Entity
{
  public required string Name { get; set; }
  public required string Description { get; set; }
  public required AnimalGender Gender { get; set; }
  public required AnimalType Type { get; set; }
  public required AnimalSize Size { get; set; }
  public required AnimalSupportType SupportType { get; set; }
  public string Photo { get; set; }
  public required int Age { get; set; }
  public string? Breed { get; set; }
  public required DateTime ShelterSince { get; set; }
  public required bool IsVaccinated { get; set; }
  public required bool IsSterilized { get; set; }
  public required bool IsUnderTreatment { get; set; }
  public required bool IsReadyTo { get; set; }

  public static Animal Create(
    string name,
    string description,
    AnimalGender gender,
    AnimalType type,
    AnimalSize size,
    AnimalSupportType supportType,
    int age,
    DateTime shelterSince,
    bool isVaccinated,
    bool isSterilized,
    bool isUnderTreatment,
    bool isReadyTo,
    string? breed
  )
  {
    return new Animal
    {
      Name = name,
      Description = description,
      Gender = gender,
      Type = type,
      Size = size,
      SupportType = supportType,
      Age = age,
      ShelterSince = shelterSince,
      IsVaccinated = isVaccinated,
      IsSterilized = isSterilized,
      IsUnderTreatment = isUnderTreatment,
      IsReadyTo = isReadyTo,
      Breed = breed
    };
  }

  public Animal UpdatePhoto(string urlPhoto)
  {
    if (string.IsNullOrEmpty(urlPhoto) || urlPhoto.IsBase64())
      throw new BadRequestError("A foto deve ser uma URL");
    
    Photo = urlPhoto;
    return this;
  }
}