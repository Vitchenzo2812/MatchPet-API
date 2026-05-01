using MatchPet.Infrastructure.Web.DTO;
using Microsoft.AspNetCore.Http;
using MatchPet.Shared.Errors;
using System.Text.Json;

namespace MatchPet.Infrastructure.Web.Middlewares;

public class GlobalExceptionMiddleware (RequestDelegate next)
{
  public async Task InvokeAsync(HttpContext context)
  {
    try
    {
      await next(context);
    }
    catch (Exception e)
    {
      await HandleExceptionAsync(context, e);
    }
  }

  private async Task HandleExceptionAsync(HttpContext context, Exception exception)
  {
    context.Response.ContentType = "application/json";

    BaseError error = new InternalServerError(exception.Message);
    
    if (exception is BaseError)
      error = (BaseError)exception;

    context.Response.StatusCode = error.HttpCode;
    var responseError = ErrorResponseDTO.FromBaseError(error);
    await context.Response.WriteAsync(JsonSerializer.Serialize(responseError));
  }
}