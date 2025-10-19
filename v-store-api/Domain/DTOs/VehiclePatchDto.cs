using System.ComponentModel.DataAnnotations;

namespace VStoreApi.Domain.DTOs;

public class VehiclePatchDto
{
  [Required(ErrorMessage = "Nome não informado")]
  [MaxLength(128, ErrorMessage = "O nome ultrapassou o limite de 128 caracteres.")]
  public string Name { get; set; } = "";

  [Required(ErrorMessage = "Marca não informada")]
  [MaxLength(64, ErrorMessage = "A marca ultrapassou o limite máximo de 64 caracteres.")]
  public string Brand { get; set; } = "";


  [Required(ErrorMessage = "Ano não informado.")]
  public int Year { get; set; }

  [Required(ErrorMessage = "modelo não informado")]
  [MaxLength(64, ErrorMessage = "O modelo ultrapassou o limite de 64 caracteres.")]
  public string Model { get; set; } = string.Empty;

  [Required(ErrorMessage = "Tipo do  veículo não informado.")]
  public int VehicleTypeId { get; set; }

  [MaxLength(64, ErrorMessage = "A cor ultrapassou o limite de 64 caracteres.")]
  [Required(ErrorMessage = "Cor não informada")]
  public string Color { get; set; } = string.Empty;

  [MaxLength(2048, ErrorMessage = "A descrição ultrapassou o limite de 2048 caracteres.")]
  public string Description { get; set; } = string.Empty;
}