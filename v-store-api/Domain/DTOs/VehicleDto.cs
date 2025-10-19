using VStoreApi.Domain.Entities;

namespace VStoreApi.Domain.DTOs;

public class VehicleDto
{
  public int Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public string Brand { get; set; } = string.Empty;
  public string Model { get; set; } = string.Empty;
  public required VehicleTypeDto VehicleType { get; set; }
  public string Color { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;
  public int Year { get; set; }

  public static VehicleDto Create(Vehicle vehicle)
  {
    return new VehicleDto()
    {
      Id = vehicle.Id,
      Name = vehicle.Name,
      Brand = vehicle.Brand,
      Year = vehicle.Year,
      Model = vehicle.Model,
      Color = vehicle.Color,
      Description = vehicle.Description,
      VehicleType = new VehicleTypeDto
      {
        Id = vehicle.VehicleType.Id,
        Name = vehicle.VehicleType.Name,
      }
    };
  }
}