namespace MatchPet.Shared.Errors;

public class InvalidEmailError() 
  : BaseError(400, "INVALID_EMAIL_FORMAT", "Invalid email format");