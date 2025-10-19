using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using VStoreApi.Domain.Contracts;
using VStoreApi.Domain.DTOs;
using VStoreApi.Infrastructure.Endpoints;

namespace VStore.Tests;

public class VehicleEndpointTests
{
  [Fact]
  public async Task GetAllVehiclesFromDatabase()
  {
    // Arrange
    var mock = new Mock<IVehicleService>();
    var vehicles = GetVehicles();
    mock.Setup(v => v.GetVehicles())
      .ReturnsAsync(vehicles);

    // Act
    var result = await VehicleEndpointHandlers.Filter(mock.Object);

    // Assert
    Assert.IsType<Ok<List<VehicleDto>>>(result);
    Assert.NotNull(result.Value);
    Assert.NotEmpty(result.Value);
    Assert.Collection(result.Value, v =>
    {
      Assert.Equal(v.Id, vehicles[0].Id);
      Assert.Equal(v.Name, vehicles[0].Name);
      Assert.Equal(v.Brand, vehicles[0].Brand);
      Assert.Equal(v.Year, vehicles[0].Year);
      Assert.Equal(v.Color, vehicles[0].Color);
      Assert.Equal(v.Model, vehicles[0].Model);
      Assert.Equal(v.Description, vehicles[0].Description);
      Assert.NotNull(v.VehicleType);
      Assert.Equal(v.VehicleType.Id, vehicles[0].VehicleType.Id);
      Assert.Equal(v.VehicleType.Name, vehicles[0].VehicleType.Name);
    }, v2 =>
    {
      Assert.Equal(v2.Id, vehicles[1].Id);
      Assert.Equal(v2.Name, vehicles[1].Name);
      Assert.Equal(v2.Brand, vehicles[1].Brand);
      Assert.Equal(v2.Year, vehicles[1].Year);
      Assert.Equal(v2.Color, vehicles[1].Color);
      Assert.Equal(v2.Model, vehicles[1].Model);
      Assert.Equal(v2.Description, vehicles[1].Description);
      Assert.NotNull(v2.VehicleType);
      Assert.Equal(v2.VehicleType.Id, vehicles[1].VehicleType.Id);
      Assert.Equal(v2.VehicleType.Name, vehicles[1].VehicleType.Name);
    });
  }

  [Fact]
  public async Task GetVehicleById()
  {
    // Arrange
    var mock = new Mock<IVehicleService>();
    var vehicles = GetVehicles();
    mock.Setup(s => s.GetVehicle(It.IsAny<int>()))
      .ReturnsAsync((int id) => vehicles.FirstOrDefault(v => v.Id == id));

    // Act
    var result = await VehicleEndpointHandlers.GetVehicle(vehicles[0].Id, mock.Object);

    // Assert
    Assert.IsType<Results<Ok<VehicleDto>, NotFound>>(result);
    var okResult = (Ok<VehicleDto>)result.Result;
    Assert.NotNull(okResult.Value);
    var v = okResult.Value;
    Assert.Equal(v.Id, vehicles[0].Id);
    Assert.Equal(v.Name, vehicles[0].Name);
    Assert.Equal(v.Brand, vehicles[0].Brand);
    Assert.Equal(v.Year, vehicles[0].Year);
    Assert.Equal(v.Color, vehicles[0].Color);
    Assert.Equal(v.Model, vehicles[0].Model);
    Assert.Equal(v.Description, vehicles[0].Description);
    Assert.NotNull(v.VehicleType);
    Assert.Equal(v.VehicleType.Id, vehicles[0].VehicleType.Id);
    Assert.Equal(v.VehicleType.Name, vehicles[0].VehicleType.Name);
  }

  [Fact]
  public async Task NotFoundVehicleById()
  {
    // Arrange
    var mock = new Mock<IVehicleService>();
    mock.Setup(s => s.GetVehicle(It.IsAny<int>()))
      .ReturnsAsync((int id) => null);

    // Act
    var result = await VehicleEndpointHandlers.GetVehicle(404, mock.Object);

    //Assert
    Assert.IsType<NotFound>(result.Result);
    Assert.NotNull(result.Result);
  }

  [Fact]
  public async Task UpdateVehicle()
  {
    // Arrange
    var mock = new Mock<IVehicleService>();
    var vehicles = GetVehicles();

    mock.Setup(s => s.PatchVehicle(It.IsAny<int>(), It.IsAny<VehiclePatchDto>()))
      .ReturnsAsync(1);


    // Act
    var result = await VehicleEndpointHandlers.PatchVehicle(1, new Mock<VehiclePatchDto>().Object, mock.Object);

    //Assert
    Assert.IsType<Ok>(result);
  }

  [Fact]
  public async Task RemoveVehicle()
  {
    // Arrange
    var mock = new Mock<IVehicleService>();
    var vehicles = GetVehicles();

    mock.Setup(s => s.DeleteVehicle(It.IsAny<int>()))
      .ReturnsAsync(1);

    // Act
    var result = await VehicleEndpointHandlers.DeleteVehicle(1, mock.Object);

    //Assert
    Assert.IsType<Ok>(result);
  }


  private static List<VehicleDto> GetVehicles()
  {
    return
    [
      new VehicleDto()
      {
        Name = "Toyota Corolla Branco Sedan",
        Brand = "Toyota",
        Year = 2020,
        Color = "Branco",
        Model = "Corolla",
        Description = "Veículo confortável e econômico.",
        VehicleType = new VehicleTypeDto()
        {
          Id = 1,
          Name = "Sedan"
        }
      },
      new VehicleDto()
      {
        Name = "Fiat Cronos Prata Sedan",
        Brand = "Fiat",
        Year = 2021,
        Color = "Prata",
        Model = "Cronos",
        Description = "Sedan elegante com bom desempenho.",
        VehicleType = new VehicleTypeDto()
        {
          Id = 1,
          Name = "Sedan"
        }
      }
    ];
  }
}