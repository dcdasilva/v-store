using VStore.Api.Domain.Services;
using VStoreApi.Domain.DTOs;
using VStoreApi.Domain.Entities;
using VStoreApi.Infrastructure.Contracts;
using VStoreApi.Infrastructure.DTOs;

namespace VStoreApi.Infrastructure.Endpoints;

public static class AuthEndpointHandlers
{
  public static async Task<IResult> Login(LoginDto dto, IUserService service, ITokenService tokenService)
  {
    var user = await service.Authenticate(dto.UserName, dto.Password);
    if (user is null) return TypedResults.Unauthorized();
      
    return TypedResults.Ok(new AuthDto()
    {
      Email = user.Email,
      Profile = user.Role == Roles.Admin ? "Admin" : "User",
      Token = tokenService.GenerateToken(user.Email, user.Role)
    });
  }
}