using System.ComponentModel.DataAnnotations;

namespace EKO.ToDoApp.Shared.Entities;

/// <summary>
/// Represents a ToDo list.
/// </summary>
public sealed class TaskList
{
    /// <summary>
    /// The unique identifier for the ToDo list.
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// The title of the ToDo list.
    /// </summary>
    [MaxLength(30)]
    public required string Title { get; set; }

    /// <summary>
    /// The owner of the ToDo list.
    /// </summary>
    public Guid UserId { get; set; }
}
