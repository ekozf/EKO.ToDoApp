using System.ComponentModel.DataAnnotations;

namespace EKO.ToDoApp.Shared.Requests.Users;

/// <summary>
/// Represents an user registration request.
/// </summary>
public sealed class RegisterUserRequest
{
    /// <summary>
    /// The name of the user.
    /// </summary>
    [MaxLength(30)]
    [Required]
    public required string FirstName { get; set; }


    /// <summary>
    /// The name of the user.
    /// </summary>
    [MaxLength(30)]
    [Required]
    public required string LastName { get; set; }

    /// <summary>
    /// The email of the user.
    /// </summary>
    [EmailAddress]
    [Required]
    public required string Email { get; set; }


    /// <summary>
    /// The password of the user.
    /// </summary>
    [MinLength(6)]
    [MaxLength(30)]
    [Required]
    public required string Password { get; set; }
}
