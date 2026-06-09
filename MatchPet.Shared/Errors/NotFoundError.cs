namespace MatchPet.Shared.Errors;

public class NotFoundError(string? message = "Not found Error")
  : BaseError(404, "NOT_FOUND_ERROR", message);