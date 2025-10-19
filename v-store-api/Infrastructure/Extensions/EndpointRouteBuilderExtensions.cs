using VStoreApi.Domain.DTOs;
using VStoreApi.Domain.Entities;
using VStoreApi.Infrastructure.Endpoints;
using VStoreApi.Infrastructure.Endpoints.Filters;

namespace VStoreApi.Infrastructure.Extensions;

public static class EndpointRouteBuilderExtensions
{
  public static void RegisterAuthEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
  {
    var authEndpoints = endpointRouteBuilder.MapGroup("auth")
      .WithTags("Segurança");
    authEndpoints.MapPost("/", AuthEndpointHandlers.Login);
  }

  public static void RegisterVehicleEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
  {
    var vehicleEndpoints = endpointRouteBuilder.MapGroup("vehicle")
      .WithTags("Gestão de Veículos");

    vehicleEndpoints.MapPost("/", VehicleEndpointHandlers.AddVehicle)
      .WithSummary("Adiciona um veículo no sistema")
      .RequireAuthorization(config => config.RequireRole([nameof(Roles.Admin)]))
      .AddEndpointFilter<ValidationFilter<VehicleCreateDto>>();

    vehicleEndpoints.MapPatch("{id}", VehicleEndpointHandlers.PatchVehicle)
      .WithSummary("Altera as informações de um veículo no sistema")
      .RequireAuthorization(config => config.RequireRole([nameof(Roles.Admin)]))
      .AddEndpointFilter<ValidationFilter<VehiclePatchDto>>();
    
    vehicleEndpoints.MapDelete("{id}", VehicleEndpointHandlers.DeleteVehicle)
      .WithSummary("Remove um veículo do sistema")
      .RequireAuthorization(config => config.RequireRole([nameof(Roles.Admin)]));

    vehicleEndpoints.MapGet("/", VehicleEndpointHandlers.Filter)
      .WithSummary("Realiza um filtro nos carros (por hora apenas retorna todos)");

    vehicleEndpoints.MapGet("{id:int}", VehicleEndpointHandlers.GetVehicle)
      .WithSummary("Recupera um veículo pelo seu ID");
  }
}