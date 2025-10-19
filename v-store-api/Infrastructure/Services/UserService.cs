using Microsoft.EntityFrameworkCore;
using VStore.Api.Domain.Services;
using VStoreApi.Domain.Entities;
using VStoreApi.Infrastructure.Data;

namespace VStoreApi.Infrastructure.Services;

public class UserService(VStoreContext context) : IUserService
{
  
  public async Task<User?> Authenticate(string email, string password)
  {
    return await context.Users
    .Where(u => u.Email == email && u.Password == password)
    .FirstOrDefaultAsync();
  }
}