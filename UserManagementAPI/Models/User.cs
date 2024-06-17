using System.ComponentModel.DataAnnotations;

namespace UserManagementAPI.Models;

public class User
{
  [Key]
  public int Id { get; set; }

  [Required]
  [MaxLength(50)]
  public string? Username { get; set; }

  [Required]
  public string? PasswordHash { get; set; }

  [MaxLength(100)]
  public string? Email { get; set; }

  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
