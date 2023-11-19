using EKO.ToDoApp.AppLogic.Services.Contracts;
using EKO.ToDoApp.Shared.Requests.TaskLists;
using Microsoft.AspNetCore.Mvc;

namespace EKO.ToDoApp.Web.Controllers;

[Route("[controller]")]
public class TaskListController : Controller
{
    private readonly ITaskListService _taskListService;

    public TaskListController(ITaskListService taskListService)
    {
        _taskListService = taskListService;
    }


    [HttpGet("get-all/{userId:guid}")]
    public async Task<IActionResult> GetAll(Guid userId)
    {
        var items = await _taskListService.GetAll(userId);

        return Ok(items);
    }
    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateTaskListRequest request)
    {
        await _taskListService.Create(request);

        return Ok();
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update(UpdateTaskListRequest request)
    {
        await _taskListService.Update(request);

        return Ok();
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(DeleteTaskListRequest request)
    {
        await _taskListService.Delete(request);

        return Ok();
    }

    [HttpPost("get-by-id")]
    public async Task<IActionResult> GetById(Guid id, Guid userId)
    {
        var item = await _taskListService.GetById(id, userId);

        return Ok(item);
    }

    [HttpPost("add-task")]
    public async Task<IActionResult> GetById(AddTaskToListRequest request)
    {
        await _taskListService.AddTask(request);

        return Ok();
    }
}
