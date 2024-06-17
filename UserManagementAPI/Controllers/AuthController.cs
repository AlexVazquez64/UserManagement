using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;
using UserManagementAPI.Services;

namespace UserManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")] // La ruta será /api/auth
public class AuthController : ControllerBase
{
  private readonly IAuthService _authService;

  public AuthController(IAuthService authService)
  {
    _authService = authService;
  }

  [HttpPost("login")]
  public async Task<ActionResult<string>> Login(LoginRequest loginRequest)
  {
    if (!ModelState.IsValid)
        return BadRequest(ModelState); // Devolver errores de validación si el modelo no es válido
        
    var token = await _authService.AuthenticateUserAsync(loginRequest);
    if (token == null)
    {
      return Unauthorized();
    }
    return Ok(token);
  }
}
