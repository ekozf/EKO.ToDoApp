namespace EKO.ToDoApp.Shared.Exceptions;

/// <summary>
/// Represents an exception thrown when the password is invalid.
/// </summary>
public sealed class InvalidPasswordException : Exception
{
    public InvalidPasswordException(string msg) : base(msg) { }
}
