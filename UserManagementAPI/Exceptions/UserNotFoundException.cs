// UserNotFoundException.cs
namespace UserManagementAPI.Exceptions;

public class UserNotFoundException : Exception
{
  public UserNotFoundException(string message) : base(message) { }
}
