using EKO.ToDoApp.Shared.Entities;
using EKO.ToDoApp.Shared.Requests.TaskLists;

namespace EKO.ToDoApp.AppLogic.Services.Contracts;

/// <summary>
/// Represents a service for managing ToDo lists.
/// </summary>
public interface ITaskListService
{
    /// <summary>
    /// Create a new List of Tasks
    /// </summary>
    /// <param name="request"><see cref="CreateTaskListRequest"/></param>
    Task Create(CreateTaskListRequest request);

    /// <summary>
    /// Deletes a List of Tasks
    /// </summary>
    /// <param name="request"><see cref="DeleteTaskListRequest"/></param>
    Task Delete(DeleteTaskListRequest request);

    /// <summary>
    /// Get all Lists of Tasks for a given user
    /// </summary>
    /// <param name="userId"><see cref="Guid"/> of the user to get the lists for</param>
    /// <returns>All list beloning to the user</returns>
    Task<IEnumerable<TaskList>> GetAll(Guid userId);

    /// <summary>
    /// Get a List of Tasks by Id
    /// </summary>
    /// <param name="id"><see cref="Guid"/> of the list</param>
    /// <param name="id"><see cref="Guid"/> of the user</param>
    /// <returns><see cref="TaskList"/></returns>
    Task<TaskList> GetById(Guid id, Guid userId);

    /// <summary>
    /// Update a List of Tasks
    /// </summary>
    /// <param name="request"><see cref="UpdateTaskListRequest"/></param>
    Task Update(UpdateTaskListRequest request);

    /// <summary>
    /// Add a task to a list
    /// </summary>
    /// <param name="request"><see cref="AddTaskToListRequest"/></param>
    Task AddTask(AddTaskToListRequest request);

}
