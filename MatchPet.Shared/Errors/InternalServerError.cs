namespace MatchPet.Shared.Errors;

public class InternalServerError (string? message = "Internal Server Error") 
  : BaseError(500, "INTERNAL_SERVER_ERROR", message);