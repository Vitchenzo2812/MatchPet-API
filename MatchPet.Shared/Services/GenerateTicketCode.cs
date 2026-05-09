namespace MatchPet.Shared.Services;

public class GenerateTicketCode
{
  private static Func<string> _generateTicketCodeFunc = GenerateDefault;
  public static string Code => _generateTicketCodeFunc();

  public static void Set(Func<string> generateTicketCodeFunc)
    => _generateTicketCodeFunc = generateTicketCodeFunc;

  private static string GenerateDefault()
  {
    const int length = 6;
    
    var random = new Random();
    var value = string.Empty;

    for (var i = 0; i < length; i++)
      value += random.Next(0, 10).ToString();

    return value;
  }
}