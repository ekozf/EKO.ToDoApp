using EKO.ToDoApp.Shared.Entities;
using EKO.ToDoApp.Shared.Requests.Tasks;

namespace EKO.ToDoApp.AppLogic.Services.Contracts;

/// <summary>
/// Service for managing shopping items.
/// </summary>
public interface ITaskService
{
    /// <summary>
    /// Get all tasks
    /// </summary>
    /// <returns><see cref="IEnumerable{ToDoItemEntity}"/> of all items</returns>
    Task<IEnumerable<ToDoTaskEntity>> GetAll();

    /// <summary>
    /// Get all tasks from a list.
    /// </summary>
    /// <param name="request">Get all tasks from list</param>
    /// <returns><see cref="IEnumerable{ToDoItemEntity}"/> of all items for that list</returns>
    Task<IEnumerable<ToDoTaskEntity>> GetAllFromList(GetAllTasksListRequest request);

    /// <summary>
    /// Get a shopping item by id.
    /// </summary>
    /// <param name="id">ID of the task</param>
    /// <param name="userId">ID of the user</param>
    /// <returns></returns>
    Task<ToDoTaskEntity?> GetById(Guid id, Guid userId);

    /// <summary>
    /// Update a shopping item.
    /// </summary>
    /// <param name="request">Updated item</param>
    Task Update(UpdateToDoTaskRequest request);

    /// <summary>
    /// Create a new shopping item.
    /// </summary>
    /// <param name="request">Details of the item</param>
    Task Create(CreateToDoTaskRequest request);

    /// <summary>
    /// Mark a shopping item as completed.
    /// </summary>
    /// <param name="request">Id of the item and family code to mark as completed</param>
    Task Complete(CompleteToDoTaskRequest request);

    /// <summary>
    /// Delete a shopping item.
    /// </summary>
    /// <param name="request">Id of the item to delete with user id and family code</param>
    Task Delete(DeleteToDoTaskRequest request);
}
