using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace MatchPet.Infrastructure.Database;

public class MatchPetDbContextDesignFactory : IDesignTimeDbContextFactory<MatchPetDbContext>
{
  public MatchPetDbContext CreateDbContext(string[] args)
  {
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

    var configuration = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json", optional: false)
      .AddJsonFile($"appsettings.{environment}.json", optional: true)
      .Build();
    
    var connectionString = configuration.GetConnectionString("MatchPet") ?? string.Empty;
    
    var optionsBuilder = new DbContextOptionsBuilder<MatchPetDbContext>();
    
    optionsBuilder
      .UseMySql(
        connectionString,
        new MySqlServerVersion(new Version())
      )
      .EnableSensitiveDataLogging()
      .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    
    return new MatchPetDbContext(optionsBuilder.Options);
  }
}