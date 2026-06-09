using MatchPet.Shared.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MatchPet.Infrastructure.Database.Converters;

public class EmailConverter() : ValueConverter<Email, string>(
  email => email.Value,
  str => Email.FromAddress(str)
);