using Microsoft.AspNetCore.Authentication.JwtBearer;
using MatchPet.Infrastructure.Web.Swagger;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using System.Text;
using MatchPet.Infrastructure.Web.Middlewares;

namespace MatchPet.Api;

public class Startup
{
  public void ConfigureServices(IServiceCollection services)
  {
    ConfigureDbContext(services);

    services
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

    // app.UseMiddleware<AuthorizationMiddleware>();
    
    app.UseEndpoints(endpoints => endpoints.MapControllers());
    
    app.UseHttpsRedirection();
  }

  private void ConfigureDbContext(IServiceCollection services)
  {
    
  }
}