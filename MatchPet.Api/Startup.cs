using Microsoft.AspNetCore.Authentication.JwtBearer;
using MatchPet.Infrastructure.Services.Contracts;
using MatchPet.Infrastructure.Web.Middlewares;
using MatchPet.Infrastructure.Web.Swagger;
using MatchPet.Infrastructure.Services;
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
  public void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
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

    services.AddStackExchangeRedisCache(opt =>
    {
      opt.Configuration = configuration["Redis:Connection"];
    });
    
    ConfigureDbContext(services, configuration);
    ConfigureDependencies(services);
    
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

  public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
  {
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

  private static void ConfigureDependencies(IServiceCollection services)
  {
    services.AddScoped<ICacheService, RedisCacheService>();
  }
  
  private static void ConfigureDbContext(IServiceCollection services, ConfigurationManager configuration)
  {
    var connectionString = configuration.GetConnectionString("MatchPet") ?? string.Empty;
    
    services.AddDbContext<MatchPetDbContext>((_, options) =>
    {
      options
        .UseMySql(
          connectionString,
          new MySqlServerVersion(new Version())
        )
        .EnableSensitiveDataLogging()
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    });
  }
}