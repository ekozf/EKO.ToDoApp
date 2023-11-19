using EKO.ToDoApp.Shared.Enums;

namespace EKO.ToDoApp.Shared.Requests.Tasks;

/// <summary>
/// Represents an action to create a new to do task
/// </summary>
public sealed class CreateToDoTaskRequest
{
    /// <summary>
    /// Id of the user creating the request
    /// </summary>
    public required Guid UserId { get; set; }

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
    public UrgencyEnum UrgencyLevel { get; set; }

    /// <summary>
    /// Id of the ToDo list that the item belongs to.
    /// </summary>
    public Guid AssociatedList { get; set; }
}
