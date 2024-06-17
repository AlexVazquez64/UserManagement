using Microsoft.AspNetCore.Mvc;
using UserManagement.API.Data; // Asegúrate de tener la referencia correcta
using UserManagement.API.Models;

namespace UserManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")] // La ruta será /api/users
public class UserController : ControllerBase
{
  private readonly AppDbContext _context;

  public UserController(AppDbContext context)
  {
    _context = context;
  }

  // GET: api/users
  [HttpGet]
  public ActionResult<IEnumerable<User>> GetUsers()
  {
    // Lógica para obtener todos los usuarios
    throw new NotImplementedException(); // Implementaremos esto más adelante
  }

  // GET: api/users/5
  [HttpGet("{id}")]
  public ActionResult<User> GetUser(int id)
  {
    // Lógica para obtener un usuario por ID
    throw new NotImplementedException();
  }

  // POST: api/users
  [HttpPost]
  public ActionResult<User> CreateUser(User user)
  {
    // Lógica para crear un nuevo usuario
    throw new NotImplementedException();
  }

  // PUT: api/users/5
  [HttpPut("{id}")]
  public IActionResult UpdateUser(int id, User user)
  {
    // Lógica para actualizar un usuario existente
    throw new NotImplementedException();
  }

  // DELETE: api/users/5
  [HttpDelete("{id}")]
  public IActionResult DeleteUser(int id)
  {
    // Lógica para eliminar un usuario
    throw new NotImplementedException();
  }
}