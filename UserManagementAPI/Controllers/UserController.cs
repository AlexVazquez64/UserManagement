using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;
using UserManagementAPI.Services;

namespace UserManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
  private readonly IUserService _userService; // Inyectamos la interfaz

  public UserController(IUserService userService) // Modificamos el constructor
  {
    _userService = userService;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<User>>> GetUsers()
  {
    var users = await _userService.GetAllUsersAsync();
    return Ok(users);
  }

  // [Authorize] // Requiere autenticación para acceder a este endpoint
  // GET: api/User/5
  [HttpGet("{id}")]
  public async Task<ActionResult<User>> GetUser(int id)
  {
    var user = await _userService.GetUserByIdAsync(id);
    return user == null ? NotFound() : Ok(user);
  }

  // POST: api/User
  [HttpPost]
  public async Task<ActionResult<User>> CreateUser(User user)
  {
    Console.WriteLine($"User");
    
    var createdUser = await _userService.CreateUserAsync(user);
    return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
  }

  // [Authorize] // Requiere autenticación para acceder a este endpoint
  // PUT: api/User/5
  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateUser(int id, User user)
  {
    if (id != user.Id)
      return BadRequest();

    await _userService.UpdateUserAsync(user);
    return NoContent();
  }

  // [Authorize] // Requiere autenticación para acceder a este endpoint
  // DELETE: api/User/5
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteUser(int id)
  {
    await _userService.DeleteUserAsync(id);
    return NoContent();
  }
}