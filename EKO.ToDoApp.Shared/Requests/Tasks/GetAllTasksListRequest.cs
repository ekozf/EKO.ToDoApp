namespace EKO.ToDoApp.Shared.Requests.Tasks;

/// <summary>
/// Represents a request to get 
/// </summary>
public sealed class GetAllTasksListRequest
{
    /// <summary>
    /// Id of the Task List to check
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// User id of which to get all tasks from
    /// </summary>
    public required Guid UserId { get; set; }
}
