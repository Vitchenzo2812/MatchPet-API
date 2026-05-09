using System.Text.RegularExpressions;
using MatchPet.Shared.Errors;

namespace MatchPet.Shared.Models;

public partial class Phone
{
  private string _value = string.Empty;

  public string Value
  {
    get => _value;

    private set
    {
      if (!IsValid(value))
        throw new InvalidPhoneError();
        
      _value = value;
    }
  }

  private Phone (string value)
  {
    ArgumentException.ThrowIfNullOrEmpty(value);
    Value = CleanValue(value);
  } 

  public static Phone Create(string value) => new(value);
  
  #region CleanValue Phone
  
  [GeneratedRegex(@"[^\d+]")]
  private static partial Regex CleanValueRegex();
  
  private string CleanValue(string value)
    => CleanValueRegex().Replace(value, "");
  
  #endregion
  
  #region Validate Phone

  [GeneratedRegex(@"^(\d{2})?9\d{8}$")]
  private static partial Regex ValidPhoneRegex();
  
  private static bool IsValid(string value)
    => ValidPhoneRegex().IsMatch(value);
    
  #endregion
}