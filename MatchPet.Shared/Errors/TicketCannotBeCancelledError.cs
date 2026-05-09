namespace MatchPet.Shared.Errors;

public class TicketCannotBeCancelledError()
  : BaseError(400, "TICKET_CANNOT_BE_CANCELLED", "Min time to request other ticket is 2 minutes");