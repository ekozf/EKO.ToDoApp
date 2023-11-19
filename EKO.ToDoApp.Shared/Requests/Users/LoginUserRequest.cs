using System.ComponentModel.DataAnnotations;

namespace EKO.ToDoApp.Shared.Requests.Users;

/// <summary>
/// Represent an user logging in request.
/// </summary>
public sealed class LoginUserRequest
{
    /// <summary>
    /// Email of the user
    /// </summary>
    [EmailAddress]
    [Required]
    public required string Email { get; set; }

    /// <summary>
    /// Password of the user
    /// </summary>
    [MinLength(6)]
    [MaxLength(30)]
    [Required]
    public required string Password { get; set; }
}
