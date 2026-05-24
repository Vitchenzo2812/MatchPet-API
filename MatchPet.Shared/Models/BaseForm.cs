namespace MatchPet.Shared.Models;

public class BaseForm : Entity
{
  public required string Name { get; set; }
  public DateTime Birth { get; set; }
  public required Email Email { get; set; }
  public required Phone Phone { get; set; }
}