namespace MatchPet.Shared.Models;

public class AdoptForm : BaseForm
{
  public required bool LivingInSaoPaulo { get; init; }
  public string Address { get; init; }
  public required string ZipCode { get; init; }
  public required string Profession { get; init; }
  public required AnimalHabitatType AnimalHabitat { get; init; }
  public required bool HasOtherAnimalsAtHome { get; init; }
  public string OtherAnimalsDescription { get; init; }
  public required string PetFood { get; init; }
  public required string VetClinic { get; init; }
  public required string AllowedRooms { get; init; }
  public required string PetAddress { get; init; }
  public required string ResponsiblePerson { get; init; }
  public required int HouseholdMembersCount { get; init; }
  public required bool HouseholdMembersAwareAndAgree { get; init; }
  public required bool AgreesWithResponsibilities { get; init; }
  public required bool HadPetsInThePast { get; init; }
  public required bool HasChildrenAtHome { get; init; }
  public required string FamilyRoutine { get; init; }
}