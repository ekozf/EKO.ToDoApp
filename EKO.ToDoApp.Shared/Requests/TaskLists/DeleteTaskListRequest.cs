namespace EKO.ToDoApp.Shared.Requests.TaskLists;

/// <summary>
/// Represents a request to delete a ToDo list.
/// </summary>
public sealed class DeleteTaskListRequest
{
    /// <summary>
    /// The unique identifier for the ToDo list.
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// The owner of the ToDo list.
    /// </summary>
    public required Guid UserId { get; set; }
}
