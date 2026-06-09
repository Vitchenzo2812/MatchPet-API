namespace MatchPet.Shared.Models;

public class Animal : Entity
{
  public required string Name { get; set; }
  public required string Description { get; set; }
  public required AnimalGender Gender { get; set; }
  public required AnimalType Type { get; set; }
  public required AnimalSize Size { get; set; }
  public required AnimalSupportType SupportType { get; set; }
  public required string Photo { get; set; }
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
    string photo,
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
      Photo = photo,
      Age = age,
      ShelterSince = shelterSince,
      IsVaccinated = isVaccinated,
      IsSterilized = isSterilized,
      IsUnderTreatment = isUnderTreatment,
      IsReadyTo = isReadyTo,
      Breed = breed
    };
  }

  public Animal UpdateName(string name)
  {
    Name = name;
    return this;
  }

  public Animal UpdateDescription(string description)
  {
    Description = description;
    return this;
  }

  public Animal UpdateGender(AnimalGender gender)
  {
    Gender = gender;
    return this;
  }

  public Animal UpdateType(AnimalType type)
  {
    Type = type;
    return this;
  }

  public Animal UpdateSize(AnimalSize size)
  {
    Size = size;
    return this;
  }

  public Animal UpdateSupportType(AnimalSupportType supportType)
  {
    SupportType = supportType;
    return this;
  }

  public Animal UpdateAge(int age)
  {
    Age = age;
    return this;
  }

  public Animal UpdateShelterSince(DateTime shelterSince)
  {
    ShelterSince = shelterSince;
    return this;
  }

  public Animal UpdateIsVaccinated(bool isVaccinated)
  {
    IsVaccinated = isVaccinated;
    return this;
  }

  public Animal UpdateIsSterilized(bool isSterilized)
  {
    IsSterilized = isSterilized;
    return this;
  }

  public Animal UpdateIsUnderTreatment(bool isUnderTreatment)
  {
    IsUnderTreatment = isUnderTreatment;
    return this;
  }

  public Animal UpdateIsReadyTo(bool isReadyTo)
  {
    IsReadyTo = isReadyTo;
    return this;
  }

  public Animal UpdateBreed(string? breed)
  {
    Breed = breed;
    return this;
  }
  
  public Animal UpdatePhoto(string photo)
  {
    Photo = photo;
    return this;
  }
}