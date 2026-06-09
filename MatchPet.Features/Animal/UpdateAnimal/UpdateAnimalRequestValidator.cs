using FluentValidation;

namespace MatchPet.Features.Animal.UpdateAnimal;

public class UpdateAnimalRequestValidator : AbstractValidator<UpdateAnimalRequest>
{
  public UpdateAnimalRequestValidator()
  {
    RuleFor(x => x.Name)
      .NotEmpty()
      .WithMessage("O nome não pode ser vazio");
        
    RuleFor(x => x.Description)
      .NotEmpty()
      .WithMessage("A descrição não pode ser vazia");
        
    RuleFor(x => x.Gender)
      .IsInEnum()
      .WithMessage("Gênero inválido");
        
    RuleFor(x => x.Type)
      .IsInEnum()
      .WithMessage("Tipo inválido");
        
    RuleFor(x => x.Size)
      .IsInEnum()
      .WithMessage("Tamanho inválido");
        
    RuleFor(x => x.SupportType)
      .IsInEnum()
      .WithMessage("Tipo de suporte inválido");
        
    RuleFor(x => x.Photo)
      .NotEmpty()
      .WithMessage("A foto não pode ser vazia");
        
    RuleFor(x => x.Age)
      .GreaterThanOrEqualTo(0)
      .WithMessage("A idade deve ser um número não negativo");
        
    RuleFor(x => x.ShelterSince)
      .LessThanOrEqualTo(DateTime.Now)
      .GreaterThan(DateTime.Now.AddYears(-120))
      .WithMessage("A data de chegada no abrigo deve estar nos últimos 120 anos e não no futuro");
        
    RuleFor(x => x.IsVaccinated)
      .NotNull()
      .WithMessage("O campo IsVaccinated não pode ser nulo");
        
    RuleFor(x => x.IsSterilized)
      .NotNull()
      .WithMessage("O campo IsSterilized não pode ser nulo");
        
    RuleFor(x => x.IsUnderTreatment)
      .NotNull()
      .WithMessage("O campo IsUnderTreatment não pode ser nulo");
        
    RuleFor(x => x.IsReadyTo)
      .Must((request, isReadyTo) =>
        !(isReadyTo && request.IsUnderTreatment))
      .WithMessage("Se o animal estiver em tratamento, ele não pode estar pronto para adoção ou apadrinhamento");
  }
}