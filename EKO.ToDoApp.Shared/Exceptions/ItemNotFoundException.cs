namespace EKO.ToDoApp.Shared.Exceptions;

/// <summary>
/// Represents an exception thrown when the item is not found.
/// </summary>
public sealed class ItemNotFoundException : Exception
{
    public ItemNotFoundException(string msg) : base(msg) { }
}
