using System.Text.RegularExpressions;
using MatchPet.Shared.Errors;

namespace MatchPet.Shared.Models;

public partial class Email
{
  private string _value = string.Empty;

  public string Value
  {
    get => _value;

    private set
    {
      if (!IsValid(value))
        throw new InvalidEmailError();
      
      _value = value;
    }
  }

  private Email(string value)
  {
    ArgumentException.ThrowIfNullOrEmpty(value);
    Value = value;
  }

  public static Email FromAddress(string address) => new(address);

  #region Validate Email

  [GeneratedRegex(@"^[\w+-\.]+@([\w-]+\.)+[\w-]{2,4}$")]
  private static partial Regex MyRegex();
  
  private static bool IsValid(string value)
    => MyRegex().IsMatch(value);

  #endregion
}