using System.Text.RegularExpressions;

namespace MatchPet.Shared.Extensions;

public static class StringExtensions
{
  public static bool IsBase64 (this string input)
  {
    var withoutSpecification = Regex.Replace(input, @"^[\w/\:.-]+;base64,", "");
    Span<byte> buffer = new Span<byte>(new byte[withoutSpecification.Length]);
    return Convert.TryFromBase64String(withoutSpecification, buffer, out int bytesParsed);
  }
}