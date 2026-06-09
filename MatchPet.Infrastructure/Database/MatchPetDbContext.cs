using MatchPet.Infrastructure.Database.Converters;
using Microsoft.EntityFrameworkCore;
using MatchPet.Shared.Models;

namespace MatchPet.Infrastructure.Database;

public class MatchPetDbContext : DbContext
{
  public DbSet<Animal> Animals { get; set; }
  public DbSet<AdoptForm> AdoptForms { get; set; }
  public DbSet<SponsorForm> SponsorForms { get; set; }
  
  public MatchPetDbContext(DbContextOptions<MatchPetDbContext> options) : base(options) {}

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    
    var emailConverter = new EmailConverter();
    var phoneConverter = new PhoneConverter();

    modelBuilder.Entity<AdoptForm>(b =>
    {
      b.Property(x => x.Email).HasConversion(emailConverter);
      b.Property(x => x.Phone).HasConversion(phoneConverter);
    });

    modelBuilder.Entity<SponsorForm>(b =>
    {
      b.Property(x => x.Email).HasConversion(emailConverter);
      b.Property(x => x.Phone).HasConversion(phoneConverter);
    });
  }
}