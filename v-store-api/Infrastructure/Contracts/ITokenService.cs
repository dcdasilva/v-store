using VStoreApi.Domain.Entities;

namespace VStoreApi.Infrastructure.Contracts;

public interface ITokenService
{
  public string GenerateToken(string email, Roles role);
}