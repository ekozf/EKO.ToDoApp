using EKO.ToDoApp.Shared.Entities;

namespace EKO.ToDoApp.Web.Models.TasksViewModel;

public sealed class CreateTaskViewModel
{
    public required IEnumerable<UrgencyViewModel> Urgencies { get; init; } 
    public required IEnumerable<TaskList> Lists { get; init; }
}
