namespace EKO.ToDoApp.Shared.Requests.Tasks;

/// <summary>
/// Represents a request to complete a ToDo task.
/// </summary>
public sealed class CompleteToDoTaskRequest
{
    /// <summary>
    /// Id of the task to complete.
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// The user id that the task belongs to.
    /// </summary>
    public required Guid UserId { get; set; }
}
