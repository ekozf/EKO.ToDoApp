namespace EKO.ToDoApp.Shared.Exceptions;

/// <summary>
/// Represents an exception thrown when the user is not found.
/// </summary>
public sealed class UserNotFoundException : Exception
{
    public UserNotFoundException(string msg) : base(msg) { }
}
