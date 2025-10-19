using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace VStoreApi.Infrastructure.Extensions;

public static class JwtExtensions
{
  public static void AddJwt(this WebApplicationBuilder builder)
  {
    var jwtConfig = builder.Configuration.GetSection("Jwt");
    var secretKey = jwtConfig["SecretKey"];
    if (secretKey is null) throw new Exception("Missing Key");
    var key = Encoding.UTF8.GetBytes(secretKey);

    builder.Services.AddAuthentication().AddJwtBearer(options =>
    {
      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtConfig["Issuer"],
        ValidAudience = jwtConfig["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
      };
    });
  }
}