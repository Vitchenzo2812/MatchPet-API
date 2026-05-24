using Microsoft.EntityFrameworkCore;
using MatchPet.Shared.Models;

namespace MatchPet.Infrastructure.Database;

public class MatchPetDbContext : DbContext
{
  public DbSet<AdoptForm> AdoptForms { get; set; }
  public DbSet<SponsorForm> SponsorForms { get; set; }
  
  public MatchPetDbContext(DbContextOptions<MatchPetDbContext> options) : base(options) {}

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
  }
}