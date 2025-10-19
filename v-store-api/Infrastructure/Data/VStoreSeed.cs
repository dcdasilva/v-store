using Microsoft.EntityFrameworkCore;
using VStoreApi.Domain.Entities;

namespace VStoreApi.Infrastructure.Data;

public static class VStoreSeed
{
  public static void Fill(ModelBuilder builder)
  {
    builder.Entity<User>().HasData(new User()
    {
      Id = 1,
      Email = "test@email.com",
      Password = "12345",
      Role = Roles.Admin
    });

    builder.Entity<VehicleType>().HasData([
      new VehicleType() { Id = 1, Name = "Sedan" },
      new VehicleType() { Id = 2, Name = "Hatchback" },
      new VehicleType() { Id = 3, Name = "SUV" },
      new VehicleType() { Id = 4, Name = "Conversível" },
      new VehicleType() { Id = 5, Name = "Van" },
      new VehicleType() { Id = 6, Name = "Pickup" },
    ]);
  }
}