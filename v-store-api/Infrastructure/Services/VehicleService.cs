using Microsoft.EntityFrameworkCore;
using VStoreApi.Domain.Contracts;
using VStoreApi.Domain.DTOs;
using VStoreApi.Domain.Entities;
using VStoreApi.Infrastructure.Data;

namespace VStoreApi.Infrastructure.Services;

public class VehicleService(VStoreContext context) : IVehicleService
{
  public async Task<int> AddVehicle(VehicleCreateDto dto)
  {
    var vehicleType = await context.VehicleTypes.FirstOrDefaultAsync(v => v.Id == dto.VehicleTypeId);
    if (vehicleType == null) throw new Exception("Tipo de veículo não encontrado");
    context.Vehicles.Add(dto.ToVehicle());
    return await context.SaveChangesAsync();
  }

  public async Task<VehicleDto?> GetVehicle(int id)
  {
    var vehicle = await context.Vehicles
      .Include(vehicle => vehicle.VehicleType)
      .FirstOrDefaultAsync(v => v.Id == id);
    return vehicle is null ? null : VehicleDto.Create(vehicle);
  }

  public async Task<List<VehicleDto>> GetVehicles()
  {
    return await context.Vehicles
      .Include(vehicle => vehicle.VehicleType)
      .Select(v => VehicleDto.Create(v))
      .ToListAsync();
  }

  public async Task<int> PatchVehicle(int id, VehiclePatchDto dto)
  {
    var vehicle = context.Vehicles.FirstOrDefault(v => v.Id == id);
    if (vehicle is null) return 0;
    vehicle.Name = dto.Name;
    vehicle.Brand = dto.Brand;
    vehicle.Year = dto.Year;
    vehicle.Model = dto.Model;
    vehicle.VehicleTypeId = dto.VehicleTypeId;
    vehicle.Color = dto.Color;
    vehicle.Description = dto.Description;
    return await context.SaveChangesAsync();
  }

  public async Task<int> DeleteVehicle(int id)
  {
    var vehicle = await context.Vehicles.FindAsync(id);
    if (vehicle is null) return 0;
    context.Vehicles.Remove(vehicle);
    return await context.SaveChangesAsync();
  }
}