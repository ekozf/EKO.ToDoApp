using EKO.ToDoApp.Shared.Enums;

namespace EKO.ToDoApp.Shared.Requests.Tasks;

public sealed class UpdateToDoTaskRequest
{
    /// <summary>
    /// The unique identifier for the ToDo item.
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// The title of the ToDo item.
    /// </summary>
    public required string Title { get; set; } = string.Empty;

    /// <summary>
    /// The description of the ToDo item.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// If the ToDo item is completed.
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// When the ToDo item is due by.
    /// </summary>
    public DateTime DueBy { get; set; }

    /// <summary>
    /// How urgent the ToDo item is.
    /// </summary>
    public UrgencyEnum Urgency { get; set; }

    /// <summary>
    /// Id of the ToDo list that the item belongs to.
    /// </summary>
    public Guid TaskListId { get; set; }

    /// <summary>
    /// Id of the user which owns the task
    /// </summary>
    public Guid UserId { get; set; }
}
