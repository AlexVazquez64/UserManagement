using System.ComponentModel.DataAnnotations;

namespace UserManagementAPI.Models;

public class User
{
  [Key]
  public int Id { get; set; }

  [Required]
  [MaxLength(50)]
  public string Username { get; set; } = string.Empty;

  [Required]
  public string PasswordHash { get; set; } = string.Empty;

  [Required]
  [MaxLength(100)]
  public string Email { get; set; } = string.Empty;

  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }

  public User()
  {
    CreatedAt = DateTime.UtcNow;
    UpdatedAt = DateTime.UtcNow;
  }
}