namespace EKO.ToDoApp.Shared.Requests.Tasks;

/// <summary>
/// Represents a request to delete a ToDo task.
/// </summary>
public sealed class DeleteToDoTaskRequest
{
    /// <summary>
    /// Id of the Task to delete
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// The user id that the task belongs to.
    /// </summary>
    public required Guid UserId { get; set; }
}
