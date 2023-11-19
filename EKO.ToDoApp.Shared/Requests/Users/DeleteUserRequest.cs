using System.ComponentModel.DataAnnotations;

namespace EKO.ToDoApp.Shared.Requests.Users;

/// <summary>
/// Request model for deleting a user.
/// </summary>
public sealed class DeleteUserRequest
{
    /// <summary>
    /// The unique identifier of the user.
    /// </summary>
    [Required]
    public required Guid Id { get; init; }
}
