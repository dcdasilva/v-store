using VStoreApi.Domain.Entities;

namespace VStore.Api.Domain.Services;

public interface IUserService
{
  public Task<User?> Authenticate(string userName, string password);
}