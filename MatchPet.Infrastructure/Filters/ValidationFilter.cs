using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace MatchPet.Infrastructure.Filters;

public class ValidationFilter<T> : IAsyncActionFilter where T : class
{
  private readonly IValidator<T> _validator;

  public ValidationFilter(IValidator<T> validator) => _validator = validator;

  public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
  {
    var request = context.ActionArguments.Values.OfType<T>().FirstOrDefault();

    if (request is not null)
    {
      var result = await _validator.ValidateAsync(request);
      if (!result.IsValid)
      {
        context.Result = new BadRequestObjectResult(result.Errors);
        return;
      }
    }

    await next();
  }
}