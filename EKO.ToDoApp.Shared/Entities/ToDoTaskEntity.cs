using EKO.ToDoApp.Shared.Enums;

namespace EKO.ToDoApp.Shared.Entities;

/// <summary>
/// Represents a single ToDo item.
/// </summary>
public sealed class ToDoTaskEntity
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
    public DateTime? DueBy { get; set; }

    /// <summary>
    /// How urgent the ToDo item is.
    /// </summary>
    public UrgencyEnum Urgency { get; set; }

    /// <summary>
    /// The user id that the item belongs to.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// The ToDo list that the item belongs to.
    /// </summary>
    public Guid TaskListId { get; set; }
}
