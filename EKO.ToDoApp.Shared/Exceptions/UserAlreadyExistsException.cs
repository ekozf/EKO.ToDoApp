namespace EKO.ToDoApp.Shared.Exceptions;

/// <summary>
/// Represents an exception thrown when a newly registering user uses an email that already is registered.
/// </summary>
public sealed class UserAlreadyExistsException : Exception
{
    public UserAlreadyExistsException(string msg) : base(msg) { }
}
