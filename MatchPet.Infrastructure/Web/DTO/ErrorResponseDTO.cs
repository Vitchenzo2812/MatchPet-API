using MatchPet.Shared.Errors;

namespace MatchPet.Infrastructure.Web.DTO;

public record ErrorResponseDTO(
  int StatusCode,
  string Message,
  string Code
)
{
  public static ErrorResponseDTO FromBaseError(BaseError error)
    => new(
        StatusCode: error.HttpCode,
        Message: error.Message,
        Code: error.Code
      );
}