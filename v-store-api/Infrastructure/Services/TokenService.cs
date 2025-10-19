using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using VStoreApi.Domain.Entities;
using VStoreApi.Infrastructure.Contracts;

namespace VStoreApi.Infrastructure.Services;

public class TokenService(IConfiguration configuration) : ITokenService
{
  public string GenerateToken(string email, Roles role)
  {
    string? secretKey = configuration["Jwt:SecretKey"];
    string? audience = configuration["Jwt:Audience"];
    string? issuer = configuration["Jwt:Issuer"];
    double expires = double.Parse(configuration["Jwt:TokenExpiry"] ?? "60");
    if (secretKey is null || audience is null || issuer is null)
      throw new Exception("token is not configured properly");
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    var token = new JwtSecurityToken(
      issuer: issuer,
      audience: audience,
      expires: DateTime.Now.AddMinutes(expires),
      signingCredentials: credentials,
      claims: [
        new Claim(ClaimTypes.Email, email),
        new Claim(ClaimTypes.Role, role == Roles.Admin ? "Admin" : "User")
      ]
    );
    
    return new JwtSecurityTokenHandler().WriteToken(token);
  }
}