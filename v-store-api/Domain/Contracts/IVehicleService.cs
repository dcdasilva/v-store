using VStoreApi.Domain.DTOs;
using VStoreApi.Domain.Entities;

namespace VStoreApi.Domain.Contracts;

public interface IVehicleService
{
  public Task<int> AddVehicle(VehicleCreateDto dto); 
  public Task<VehicleDto?> GetVehicle(int id);
  public Task<List<VehicleDto>> GetVehicles();
  public Task<int> PatchVehicle(int id, VehiclePatchDto dto);
  public Task<int> DeleteVehicle(int id);
}