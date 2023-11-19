using System.ComponentModel.DataAnnotations;

namespace EKO.ToDoApp.Shared.Requests.TaskLists;

/// <summary>
/// Represents a request to create a new ToDo list.
/// </summary>
public sealed class CreateTaskListRequest
{
    /// <summary>
    /// The title of the ToDo list.
    /// </summary>
    [MaxLength(30)]
    public required string Title { get; set; }

    /// <summary>
    /// The owner of the ToDo list.
    /// </summary>
    public required Guid UserId { get; set; }
}
