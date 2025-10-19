using System.ComponentModel.DataAnnotations;

namespace VStoreApi.Domain.DTOs;

public class LoginDto
{
  [Required(ErrorMessage = "Username não informado.")]
  public string UserName { get; set; } = "";
  
  [Required(ErrorMessage = "Senha não informada.")]
  public string Password { get; set; } = "";
}