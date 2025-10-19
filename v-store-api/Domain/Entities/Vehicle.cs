using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VStoreApi.Domain.Entities;

public class Vehicle : Entity
{
  [Required] [StringLength(128)] public string Name { get; set; } = string.Empty;
  [Required] [StringLength(64)] public string Brand { get; set; } = string.Empty;

  [Required] [StringLength(64)] public string Model { get; set; } = string.Empty;

  public VehicleType? VehicleType { get; set; }
  public int VehicleTypeId { get; set; }

  [Required] [StringLength(32)] public required string Color { get; set; }

  [StringLength(2048)] public required string Description { get; set; }

  [Required] public int Year { get; set; }
}