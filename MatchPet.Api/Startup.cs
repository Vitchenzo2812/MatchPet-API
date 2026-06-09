using Microsoft.AspNetCore.Authentication.JwtBearer;
using MatchPet.Infrastructure.Web.Middlewares;
using MatchPet.Infrastructure.Web.Swagger;
using MatchPet.Infrastructure.Database;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MatchPet.Api.Extensions;
using System.Text;

namespace MatchPet.Api;

public class Startup
{
  public void ConfigureServices(IServiceCollection services)
  {
    services
      .AddDependencyInversion()
      .AddValidators()
      .AddControllers()
      .AddJsonOptions(opt => opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
    
    services.AddSwaggerGen(opt =>
    {
      opt.AddJwtAuth();
      opt.SwaggerDoc(
        "matchPet",
        new OpenApiInfo
        {
          Title = "MatchPet",
          Version = "v1",
        }
      );
    });

    services.AddCors(opt =>
    {
      opt.AddDefaultPolicy(policy =>
      {
        policy
          .AllowAnyHeader()
          .AllowAnyOrigin()
          .AllowAnyMethod();
      });
    });
    
    ConfigureDbContext(services);
    
    var secret = Environment.GetEnvironmentVariable("JWT_SECRET");

    if (string.IsNullOrEmpty(secret))
      return;

    services
      .AddAuthentication(opt =>
      {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(opt =>
      {
        opt.RequireHttpsMetadata = false;
        opt.SaveToken = true;
        opt.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret)),
          ValidateIssuer = false,
          ValidateAudience = false
        };
      });
  }

  private void MigrateDatabase (IApplicationBuilder app)
  {
    var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<MatchPetDbContext>();
    dbContext.Database.Migrate();
  }
  
  public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
  {
    MigrateDatabase(app);
    
    if (env.IsDevelopment())
      app.UseDeveloperExceptionPage();

    app.UseCors();
    
    app.UseMiddleware<GlobalExceptionMiddleware>();

    app.UseSwagger();
    app.UseSwaggerUI(config =>
    {
      config.SwaggerEndpoint("/swagger/matchPet/swagger.json", "MatchPet API");
    });
    
    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();
    
    app.UseEndpoints(endpoints => endpoints.MapControllers());
    
    app.UseHttpsRedirection();
  }
  
  private static void ConfigureDbContext(IServiceCollection services)
  {
    services.AddDbContext<MatchPetDbContext>((_, options) =>
    {
      options
        .UseMySql(
          Environment.GetEnvironmentVariable("MYSQL_MATCHPET_CONNECTION_STRING"),
          new MySqlServerVersion(new Version()),
          opt => opt.EnableRetryOnFailure()
        )
        .EnableSensitiveDataLogging()
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    });
  }
}