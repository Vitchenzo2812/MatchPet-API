using MatchPet.Infrastructure.Repository.Contracts;
using MatchPet.Infrastructure.Database.Contracts;
using MatchPet.Infrastructure.Services.Contracts;
using MatchPet.Features.Animal.CreateAnimal;
using MatchPet.Infrastructure.Repository;
using MatchPet.Infrastructure.Services;
using MatchPet.Infrastructure.Database;
using MatchPet.Infrastructure.Filters;
using FluentValidation;

namespace MatchPet.Api.Extensions;

public static class ServiceExtensions
{
  public static IServiceCollection AddDependencyInversion(this IServiceCollection services)
  {
    return services
      .AddScoped(typeof(ValidationFilter<>))
      .AddScoped<IValidator<CreateAnimalRequest>, CreateAnimalRequestValidator>()
      .AddScoped<IFileManagerService, S3ManagerService>()
      .AddScoped<IUnitOfWork, UnitOfWork>()
      .AddScoped<IAnimalRepository, AnimalRepository>();
  }

  public static IServiceCollection AddValidators(this IServiceCollection services)
  {
    return services
      .AddValidatorsFromAssemblyContaining<CreateAnimalRequestValidator>();
  }
}