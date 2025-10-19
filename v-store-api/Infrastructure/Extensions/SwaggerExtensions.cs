using Microsoft.OpenApi.Models;

namespace VStoreApi.Infrastructure.Extensions;

public static class SwaggerExtensions
{
  public static void AddVStoreSwagger(this IServiceCollection services)
  {
    services.AddSwaggerGen(options =>
    {
      options.SwaggerDoc("v1", new OpenApiInfo
      {
        Title = "API V-Store",
        Version = "v1",
        Description = "API para demonstração e estudo do ASP.NET Core Minimal Api",
      });
      options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
      {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Adicione o token no formato: 'Bearer <JWT Token>.\nExemplo: Bearer eyJhbGciOiJI..."
      });
      options.AddSecurityRequirement(new OpenApiSecurityRequirement
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
            {
              Type = ReferenceType.SecurityScheme,
              Id = "Bearer"
            }
          },
          new string[] {}
        }
      });
    });
    
  }
}