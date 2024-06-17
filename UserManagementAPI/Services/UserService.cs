using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Data;
using UserManagementAPI.Models;

namespace UserManagementAPI.Services;

public class UserService : IUserService
{
  private readonly AppDbContext _context;

  public UserService(AppDbContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<User>> GetAllUsersAsync()
  {
    return await _context.Users
        .FromSqlRaw("EXECUTE sp_GetUsers")
        .ToListAsync();
  }

  public async Task<User?> GetUserByIdAsync(int id)
  {
    var param = new SqlParameter("@Id", id);
    return await _context.Users
        .FromSqlRaw("EXECUTE sp_GetUserById @Id", param)
        .SingleOrDefaultAsync();
  }

  public async Task<User> CreateUserAsync(User user)
  {
    user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

    var usernameParam = new SqlParameter("@Username", user.Username);
    var passwordHashParam = new SqlParameter("@PasswordHash", user.PasswordHash);
    var emailParam = new SqlParameter("@Email", user.Email ?? (object)DBNull.Value);

    var result = await _context.Users
        .FromSqlRaw("EXECUTE sp_CreateUser @Username, @PasswordHash, @Email",
            usernameParam, passwordHashParam, emailParam)
        .ToListAsync();

    user.Id = result.First().Id; // Obtener el ID generado desde la consulta
    return user;
  }

  public async Task UpdateUserAsync(User user)
  {
    user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

    var idParam = new SqlParameter("@Id", user.Id);
    var usernameParam = new SqlParameter("@Username", user.Username);
    var passwordHashParam = new SqlParameter("@PasswordHash", user.PasswordHash);
    var emailParam = new SqlParameter("@Email", user.Email ?? (object)DBNull.Value);

    await _context.Database
        .ExecuteSqlRawAsync("EXECUTE sp_UpdateUser @Id, @Username, @PasswordHash, @Email",
            idParam, usernameParam, passwordHashParam, emailParam);
  }

  public async Task DeleteUserAsync(int id)
  {
    var param = new SqlParameter("@Id", id);
    await _context.Database.ExecuteSqlRawAsync("EXECUTE sp_DeleteUser @Id", param);
  }

  public async Task<User?> GetUserByUsernameAsync(string username)
  {
    var param = new SqlParameter("@Username", username);
    return await _context.Users
        .FromSqlRaw("SELECT * FROM Users WHERE Username = @Username", param) // Consulta directa para obtener por nombre de usuario
        .SingleOrDefaultAsync();
  }
}


// Definición de la interfaz IUserService
public interface IUserService
{
  Task<IEnumerable<User>> GetAllUsersAsync();
  Task<User?> GetUserByIdAsync(int id);
  Task<User> CreateUserAsync(User user);
  Task UpdateUserAsync(User user);
  Task DeleteUserAsync(int id);
  Task<User?> GetUserByUsernameAsync(string username);
}
