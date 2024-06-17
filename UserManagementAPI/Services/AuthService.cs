using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManagementAPI.Exceptions;
using UserManagementAPI.Models;

namespace UserManagementAPI.Services;

public class AuthService : IAuthService
{
  private readonly IConfiguration _configuration;
  private readonly IUserService _userService;

  public AuthService(IConfiguration configuration, IUserService userService)
  {
    _configuration = configuration;
    _userService = userService;
  }

  public async Task<string?> AuthenticateUserAsync(LoginRequest loginRequest)
  {
    Console.WriteLine($"loginRequest");
    
    var user = await _userService.GetUserByUsernameAsync(loginRequest.Username) ?? throw new UserNotFoundException("El usuario no existe."); // Asumiendo que tienes un método GetUserByUsernameAsync en UserService

    if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash))
      throw new InvalidCredentialsException("Credenciales inválidas.");

    // Autenticación exitosa, generar token JWT
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
      Expires = DateTime.UtcNow.AddDays(7),
      SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };
    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
  }
}

// Definición de la interfaz IAuthService
public interface IAuthService
{
  Task<string?> AuthenticateUserAsync(LoginRequest loginRequest);
}
