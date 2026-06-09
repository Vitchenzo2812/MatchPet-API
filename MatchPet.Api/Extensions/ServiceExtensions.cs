using MatchPet.Infrastructure.Repository.Contracts;
using MatchPet.Infrastructure.Database.Contracts;
using MatchPet.Features.Animal.GetAnimalById;
using MatchPet.Features.Animal.DeleteAnimal;
using MatchPet.Features.Animal.CreateAnimal;
using MatchPet.Features.Animal.UpdateAnimal;
using MatchPet.Features.Animal.GetAnimals;
using MatchPet.Infrastructure.Repository;
using MatchPet.Infrastructure.Database;
using MatchPet.Infrastructure.Filters;
using MatchPet.Features;
using FluentValidation;

namespace MatchPet.Api.Extensions;

public static class ServiceExtensions
{
  public static IServiceCollection AddDependencyInversion(this IServiceCollection services)
  {
    return services
      .AddScoped<IBaseHandler<GetAnimalsRequest, List<GetAnimalsResponse>>, GetAnimalsHandler>()
      .AddScoped<IBaseHandler<Guid, GetAnimalByIdResponse>, GetAnimalByIdHandler>()
      .AddScoped<IBaseHandler<CreateAnimalRequest>, CreateAnimalHandler>()
      .AddScoped<IBaseHandler<UpdateAnimalRequest>, UpdateAnimalHandler>()
      .AddScoped<IBaseHandler<Guid>, DeleteAnimalHandler>()
      .AddScoped(typeof(ValidationFilter<>))
      .AddScoped<IValidator<CreateAnimalRequest>, CreateAnimalRequestValidator>()
      .AddScoped<IValidator<UpdateAnimalRequest>, UpdateAnimalRequestValidator>()
      .AddScoped<IUnitOfWork, UnitOfWork>()
      .AddScoped<IAnimalRepository, AnimalRepository>();
  }

  public static IServiceCollection AddValidators(this IServiceCollection services)
  {
    return services
      .AddValidatorsFromAssemblyContaining<CreateAnimalRequestValidator>();
  }
}