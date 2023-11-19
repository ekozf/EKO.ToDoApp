using EKO.ToDoApp.Shared.Entities;

namespace EKO.ToDoApp.Web.Models.TasksViewModel;

public sealed class TaskDetailsViewModel
{
    public required IEnumerable<UrgencyViewModel> Urgencies { get; init; }
    public required IEnumerable<TaskList> Lists { get; init; }
    public required ToDoTaskEntity Task { get; init; }
}
