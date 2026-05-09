namespace MatchPet.Shared.Models;

public class User : Entity
{
  public required string Name { get; set; }
  public required Email Email { get; set; }
  public required ProfileType ProfileType { get; set; }
  public required Phone Phone { get; set; }
  public DateTime Birth { get; set; }
  public bool EmailValidated { get; set; }
  public string PasswordHash { get; set; } = string.Empty;

  private readonly List<Address> addresses = [];
  public IReadOnlyCollection<Address> Addresses => addresses;
}