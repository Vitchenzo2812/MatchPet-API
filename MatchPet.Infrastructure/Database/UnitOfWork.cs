using MatchPet.Infrastructure.Database.Contracts;

namespace MatchPet.Infrastructure.Database;

public class UnitOfWork (MatchPetDbContext db) : IUnitOfWork
{
  public async Task SaveChangesAsync()
    => await db.SaveChangesAsync();
}