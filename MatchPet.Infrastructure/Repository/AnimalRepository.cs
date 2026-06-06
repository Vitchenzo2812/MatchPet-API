using MatchPet.Infrastructure.Repository.Contracts;
using MatchPet.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using MatchPet.Shared.Models;

namespace MatchPet.Infrastructure.Repository;

public class AnimalRepository (MatchPetDbContext db) : IAnimalRepository
{
  public async Task<Animal?> GetById(Guid id)
  {
    return await db.Animals
      .AsTracking()
      .FirstOrDefaultAsync(x => x.Id == id);
  }

  public void Save(Animal animal) => db.Animals.Add(animal);
  public void Delete(Animal animal) => db.Animals.Remove(animal);
}