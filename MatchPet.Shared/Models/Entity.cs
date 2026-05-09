using System.ComponentModel.DataAnnotations;

namespace MatchPet.Shared.Models;

public class Entity
{
  [Key]
  public Guid Id { get; set; } = Guid.NewGuid();
}