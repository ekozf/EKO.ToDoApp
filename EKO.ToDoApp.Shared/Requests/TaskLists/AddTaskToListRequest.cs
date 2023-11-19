namespace EKO.ToDoApp.Shared.Requests.TaskLists;

/// <summary>
/// Represents a request to add a Task to a ToDo list.
/// </summary>
public sealed class AddTaskToListRequest
{
    /// <summary>
    ///    /// The unique identifier for the ToDo list.
    /// </summary>
    public Guid TaskListId { get; set; }

    /// <summary>
    /// The unique identifier for the Task.
    /// </summary>
    public Guid TaskId { get; set; }

    /// <summary>
    /// The user id that the task belongs to.
    /// </summary>
    public Guid UserId { get; set; }
}
