using MatchPet.Shared.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MatchPet.Infrastructure.Database.Converters;

public class PhoneConverter() : ValueConverter<Phone, string>(
  phone => phone.Value,
  str => Phone.Create(str)
);