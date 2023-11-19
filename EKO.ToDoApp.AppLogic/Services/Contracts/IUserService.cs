using EKO.ToDoApp.Shared.Entities;
using EKO.ToDoApp.Shared.Requests.Users;

namespace EKO.ToDoApp.AppLogic.Services.Contracts;

/// <summary>
/// Service for managing users.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Create a new user.
    /// </summary>
    /// <param name="user">User to create</param>
    /// <returns>Created user</returns>
    Task<UserEntity> CreateNewUser(RegisterUserRequest newUser);

    /// <summary>
    /// Get all users.
    /// </summary>
    /// <returns><see cref="IEnumerable{UserModel}"/> of all users</returns>
    Task<IEnumerable<UserEntity>> GetAllUsers();

    /// <summary>
    /// Get an user by id.
    /// </summary>
    /// <param name="email">Email of the user</param>
    /// <param name="password">Password of the user</param>
    /// <returns></returns>
    Task<UserEntity> GetUserByEmailAndPassword(string email, string password);

    /// <summary>
    /// Update an user.
    /// </summary>
    /// <param name="user">Updated details</param>
    /// <returns>Updated user</returns>
    Task<UserEntity> UpdateUser(UserEntity updatedUser);

    /// <summary>
    /// Delete an user.
    /// </summary>
    /// <param name="id">Id to delete</param>
    /// <returns></returns>
    Task DeleteUser(Guid id);
}
