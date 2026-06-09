using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace MatchPet.Infrastructure.Database;

public class MatchPetDbContextDesignFactory : IDesignTimeDbContextFactory<MatchPetDbContext>
{
  public MatchPetDbContext CreateDbContext(string[] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<MatchPetDbContext>();

    optionsBuilder
      .UseMySql(
        Environment.GetEnvironmentVariable("MYSQL_MATCHPET_CONNECTION_STRING")!,
        new MySqlServerVersion(new Version()),
        opt => opt.EnableRetryOnFailure()
      )
      .EnableSensitiveDataLogging()
      .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    
    return new MatchPetDbContext(optionsBuilder.Options);
  }
}