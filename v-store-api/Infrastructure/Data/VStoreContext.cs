using Microsoft.EntityFrameworkCore;
using VStoreApi.Domain.Entities;

namespace VStoreApi.Infrastructure.Data;

public class VStoreContext(DbContextOptions<VStoreContext> options) : DbContext(options)
{
  public DbSet<Vehicle> Vehicles { get; set; }
  public DbSet<User> Users { get; set; }
  public DbSet<VehicleType> VehicleTypes { get; set; }


  protected override void OnModelCreating(ModelBuilder builder)
  {
    VStoreSeed.Fill(builder);
  }
}