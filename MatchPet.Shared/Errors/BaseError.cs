namespace MatchPet.Shared.Errors;

public class BaseError : Exception
{
  public int HttpCode { get; set; }
  public string Code { get; set; }

  public BaseError(int httpCode, string code, string? message) : base(message)
  {
    HttpCode = httpCode;
    Code = code;
  }
}