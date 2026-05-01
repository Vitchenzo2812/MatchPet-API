using MatchPet.Infrastructure.Web.Swagger.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MatchPet.Infrastructure.Web.Swagger;

public static class SwaggerExtension
{
  private static readonly string authType = "Bearer JWT";

  private static readonly OpenApiSecurityRequirement requirement = new()
    {{
      new OpenApiSecurityScheme
      {
        Reference = new OpenApiReference
        {
          Type = ReferenceType.SecurityScheme,
          Id = authType
        }
      },
      []
    }};

  private static readonly OpenApiSecurityScheme scheme = new()
  {
    In = ParameterLocation.Header,
    Description = "Please enter a valid token",
    Name = "Authorization",
    Type = SecuritySchemeType.Http,
    BearerFormat = "JWT",
    Scheme = "Bearer"
  };

  public static SwaggerGenOptions AddJwtAuth(this SwaggerGenOptions options)
  {
    options.AddSecurityDefinition(authType, scheme);
    options.OperationFilter<SecurityRequirementsOperationFilter>();
    
    return options;
  }
  
  private class SecurityRequirementsOperationFilter : IOperationFilter
  {
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
      if (
        (context.MethodInfo.GetCustomAttributes(true).Any(x => x is SwaggerAuthorizeAttribute)) ||
        (context.MethodInfo.DeclaringType?.GetCustomAttributes(true).Any(x => x is SwaggerAuthorizeAttribute) ?? false)
      )
      {
        operation.Security = new List<OpenApiSecurityRequirement>
        {
          requirement
        };
      }
    }
  }
}