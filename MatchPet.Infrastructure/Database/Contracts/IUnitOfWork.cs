namespace MatchPet.Infrastructure.Database.Contracts;

public interface IUnitOfWork
{
  Task SaveChangesAsync();
}