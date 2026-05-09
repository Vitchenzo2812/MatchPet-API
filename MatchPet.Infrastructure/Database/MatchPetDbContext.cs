using Microsoft.EntityFrameworkCore;
using MatchPet.Shared.Models;

namespace MatchPet.Infrastructure.Database;

public class MatchPetDbContext : DbContext
{
  public DbSet<User> Users { get; set; }
  public DbSet<Ticket> Tickets { get; set; }
  public DbSet<Address> Addresses { get; set; }
  
  public MatchPetDbContext(DbContextOptions<MatchPetDbContext> options) : base(options) {}

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
  }
}