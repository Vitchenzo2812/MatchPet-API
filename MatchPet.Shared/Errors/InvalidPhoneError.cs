namespace MatchPet.Shared.Errors;

public class InvalidPhoneError()
  : BaseError(400, "INVALID_PHONE_FORMAT", "Invalid phone format");