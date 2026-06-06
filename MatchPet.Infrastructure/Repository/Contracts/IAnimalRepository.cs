using MatchPet.Shared.Models;

namespace MatchPet.Infrastructure.Repository.Contracts;

public interface IAnimalRepository
{
  Task<Animal?> GetById(Guid id);
  void Save(Animal animal);
  void Delete(Animal animal);
}