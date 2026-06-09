using MatchPet.Infrastructure.Database;
using MatchPet.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace MatchPet.Features.Animal.GetAnimals;

public class GetAnimalsHandler (
  MatchPetDbContext db
) : IBaseHandler<GetAnimalsRequest, List<GetAnimalsResponse>>
{
  public async Task<List<GetAnimalsResponse>> Handle(GetAnimalsRequest request)
  {
    var query = db.Animals.AsNoTracking().AsQueryable();

    if (request.Type.HasValue)
      query = query.Where(x => x.Type == request.Type.Value);

    if (request.Gender.HasValue)
      query = query.Where(x => x.Gender == request.Gender.Value);

    if (request.SupportType.HasValue)
      query = query.Where(x => x.SupportType == request.SupportType.Value);

    if (request.IsVaccinated.HasValue)
      query = query.Where(x => x.IsVaccinated == request.IsVaccinated.Value);

    if (request.IsSterilized.HasValue)
      query = query.Where(x => x.IsSterilized == request.IsSterilized.Value);

    if (request.IsUnderTreatment.HasValue)
      query = query.Where(x => x.IsUnderTreatment == request.IsUnderTreatment.Value);

    if (request.IsReadyTo.HasValue)
      query = query.Where(x => x.IsReadyTo == request.IsReadyTo.Value);

    if (request.AgeRange.HasValue)
      query = request.AgeRange.Value switch
      {
        AnimalAgeRange.LessThanOneYear  => query.Where(x => x.Age < 1),
        AnimalAgeRange.OneToFiveYears   => query.Where(x => x.Age >= 1 && x.Age <= 5),
        AnimalAgeRange.SixToTenYears    => query.Where(x => x.Age >= 6 && x.Age <= 10),
        AnimalAgeRange.MoreThanTenYears => query.Where(x => x.Age > 10),
        _ => query
      };

    var animals = await query
      .Select(x => GetAnimalsResponse.FromEntity(x))
      .ToListAsync();

    return animals;
  }
}