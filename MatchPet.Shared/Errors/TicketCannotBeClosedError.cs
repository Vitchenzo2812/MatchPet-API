namespace MatchPet.Shared.Errors;

public class TicketCannotBeClosedError()
  : BaseError(400, "TICKET_CANNOT_BE_CLOSED", "Only ADMIN can close tickets");