using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MatchPet.Shared.Models;

namespace MatchPet.Infrastructure.Database.Mappers;

public class UserMapper : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.ToTable("Users");

    builder.HasKey(u => u.Id);

    builder
      .Property(u => u.Email)
      .HasColumnName("Email")
      .HasConversion(
        email => email.Value,
        value => Email.FromAddress(value)
      );

    builder
      .Property(u => u.Phone)
      .HasColumnName("Phone")
      .HasConversion(
        phone => phone.Value,
        value => Phone.Create(value)
      );
  }
}