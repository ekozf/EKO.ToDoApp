using System.ComponentModel.DataAnnotations;

namespace EKO.ToDoApp.Shared.Entities;

/// <summary>
/// Represents a single user.
/// </summary>
public sealed class UserEntity
{
    /// <summary>
    /// The unique identifier for the user.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// First name of the user.
    /// </summary>
    [MaxLength(30)]
    public required string FirstName { get; set; }

    /// <summary>
    /// Last name of the user.
    /// </summary>
    [MaxLength(30)]
    public required string LastName { get; set; }

    /// <summary>
    /// Email of the user.
    /// </summary>
    [EmailAddress]
    public required string Email { get; set; }

    /// <summary>
    /// Password of the user.
    /// </summary>
    [MinLength(6)]
    public required string Password { get; set; }

    /// <summary>
    /// Salt for the password.
    /// </summary>
    public required string Salt { get; set; }
}
