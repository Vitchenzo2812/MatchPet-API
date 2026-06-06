namespace MatchPet.Shared.Errors;

public class BadRequestError(string? message = "Bad Request Error") 
  : BaseError(400, "BAD_REQUEST_ERROR", message);