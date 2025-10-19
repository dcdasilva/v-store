using Microsoft.AspNetCore.Http.HttpResults;
using VStoreApi.Domain.Contracts;
using VStoreApi.Domain.DTOs;
using VStoreApi.Domain.Entities;

namespace VStoreApi.Infrastructure.Endpoints;

public static class VehicleEndpointHandlers
{
  public static async Task<IResult> AddVehicle(VehicleCreateDto dto, IVehicleService vehicleService)
  {
    int result = await vehicleService.AddVehicle(dto);

    // TODO: Add the correct path
    return result <= 0
      ? Results.BadRequest()
      : TypedResults.Accepted("");
  }

  public static async Task<Ok<List<VehicleDto>>> Filter(IVehicleService vehicleService)
  {
    return TypedResults.Ok(await vehicleService.GetVehicles());
  }

  public static async Task<Results<Ok<VehicleDto>, NotFound>> GetVehicle(int id, IVehicleService vehicleService)
  {
    var vehicle = await vehicleService.GetVehicle(id);
    if (vehicle is null) return TypedResults.NotFound();
    return TypedResults.Ok(vehicle);
  }

  public static async Task<IResult> PatchVehicle(int id, VehiclePatchDto dto, IVehicleService vehicleService)
  {
    var result = await vehicleService.PatchVehicle(id, dto);
    return result <= 0 ? TypedResults.NotFound() : TypedResults.Ok();
  }

  public static async Task<IResult> DeleteVehicle(int id, IVehicleService vehicleService)
  {
    await vehicleService.DeleteVehicle(id);
    return TypedResults.Ok();
  }
}