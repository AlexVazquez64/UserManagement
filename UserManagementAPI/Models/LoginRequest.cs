using System.ComponentModel.DataAnnotations;

namespace UserManagementAPI.Models;

public class LoginRequest
{
  [Required]
  [StringLength(50, MinimumLength = 3)] // Longitud mínima y máxima para el nombre de usuario
  public string Username { get; set; } = string.Empty;

  [Required]
  [StringLength(100, MinimumLength = 6)] // Longitud mínima y máxima para la contraseña
  public string Password { get; set; } = string.Empty;
}
