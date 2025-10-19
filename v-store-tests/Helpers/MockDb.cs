using Microsoft.EntityFrameworkCore;
using VStoreApi.Infrastructure.Data;

namespace VStore.Tests.Helpers;

// Source: https://github.com/dotnet/AspNetCore.Docs.Samples/blob/main/fundamentals/minimal-apis/samples/MinApiTestsSample/UnitTests/Helpers/MockDb.cs
public class MockDb : IDbContextFactory<VStoreContext>
{
  public VStoreContext CreateDbContext()
  {
    var options = new DbContextOptionsBuilder<VStoreContext>()
      .UseInMemoryDatabase($"InMemoryTestDb-{DateTime.Now.ToFileTimeUtc()}")
      .Options;

    return new VStoreContext(options);
  }
}