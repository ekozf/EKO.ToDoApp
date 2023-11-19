using System.ComponentModel.DataAnnotations;

namespace EKO.ToDoApp.Shared.Requests.TaskLists;

public sealed class UpdateTaskListRequest
{
    /// <summary>
    /// The unique identifier for the ToDo list.
    /// </summary>
    public Guid Id { get; set; }

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
