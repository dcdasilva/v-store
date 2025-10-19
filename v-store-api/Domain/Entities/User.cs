using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace VStoreApi.Domain.Entities;

[Index(nameof(Email), IsUnique = true)]
public class User : Entity
{
  [Required] [StringLength(64)] public string FirstName { get; set; } = string.Empty;

  [StringLength(128)] public string LastName { get; set; } = string.Empty;
  [Required] [StringLength(256)] public string Email { get; set; } = string.Empty;
  [Required] [StringLength(64)] public string Password { get; set; } = string.Empty;
  [Required]  public Roles Role { get; set; }
}