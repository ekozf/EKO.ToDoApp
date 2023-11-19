using EKO.ToDoApp.AppLogic.Services.Contracts;
using EKO.ToDoApp.Shared.Enums;
using EKO.ToDoApp.Shared.Exceptions;
using EKO.ToDoApp.Shared.Requests.Tasks;
using EKO.ToDoApp.Web.Models.TasksViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EKO.ToDoApp.Web.Controllers;

[Authorize]
public class TasksController : Controller
{
    private readonly ITaskService _taskService;
    private readonly ITaskListService _taskListService;

    public TasksController(ITaskService taskService, ITaskListService taskListService)
    {
        _taskService = taskService;
        _taskListService = taskListService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var parsed = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId);

        if (!parsed)
        {
            return Redirect("/Account/Login");
        }

        var list = await _taskListService.GetAll(userId);

        if (!list.Any())
        {
            return Redirect("/Account/Login");
        }

        var items = await _taskService.GetAllFromList(new GetAllTasksListRequest
        {
            Id = list.First(x => x.Title == "Inbox").Id,
            UserId = userId
        });

        items = items
            .Where(x => !x.IsCompleted)
            .OrderByDescending(x => x.DueBy)
            .ThenByDescending(x => x.Urgency)
            .ToList();

        return View(items);
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
    {
        var items = await _taskService.GetAll();

        return Ok(items);
    }


    [HttpPost("get-all-list")]
    public async Task<IActionResult> GetAllFamily(GetAllTasksListRequest request)
    {
        var items = await _taskService.GetAllFromList(request);

        return Ok(items);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var parsed = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId);

        if (!parsed)
        {
            return Redirect("/Account/Login");
        }

        try
        {
            var urgencies = Enum.GetValues(typeof(UrgencyEnum))
                .Cast<UrgencyEnum>()
                .Select(x => new UrgencyViewModel
                {
                    Urgency = x.ToString(),
                    UrgencyId = (int)x
                });

            var lists = await _taskListService.GetAll(userId);

            var item = await _taskService.GetById(id, userId);

            var vm = new TaskDetailsViewModel
            {
                Lists = lists,
                Urgencies = urgencies,
                Task = item!
            };

            return View(vm);
        }
        catch (ItemNotFoundException)
        {
            return RedirectToAction("Index");
        }
    }

    [HttpGet("create")]
    public async Task<IActionResult> Create()
    {
        var urgencies = Enum.GetValues(typeof(UrgencyEnum))
            .Cast<UrgencyEnum>()
            .Select(x => new UrgencyViewModel
            {
                Urgency = x.ToString(),
                UrgencyId = (int)x
            });

        var parsed = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId);

        if (!parsed)
        {
            return Redirect("/Account/Login");
        }

        var lists = await _taskListService.GetAll(userId);

        var vm = new CreateTaskViewModel
        {
            Urgencies = urgencies,
            Lists = lists
        };

        return View(vm);
    }


    [HttpPost("create-task")]
    public async Task<IActionResult> CreateTask([FromForm] CreateToDoTaskRequest model)
    {
        var parsed = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId);

        if (!parsed)
        {
            return Redirect("/Account/Login");
        }

        model.UserId = userId;

        await _taskService.Create(model);

        return Ok();
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromForm] UpdateToDoTaskRequest request)
    {
        var parsed = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId);

        if (!parsed)
        {
            return Redirect("/Account/Login");
        }

        request.UserId = userId;

        await _taskService.Update(request);

        return Ok();
    }

    [HttpPost("complete")]
    public async Task<IActionResult> Complete([FromForm] CompleteToDoTaskRequest request)
    {
        var parsed = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId);

        if (!parsed)
        {
            return Redirect("/Account/Login");
        }

        request.UserId = userId;

        await _taskService.Complete(request);

        return Ok();
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromForm] DeleteToDoTaskRequest request)
    {
        var parsed = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId);

        if (!parsed)
        {
            return Redirect("/Account/Login");
        }

        request.UserId = userId;

        await _taskService.Delete(request);

        return Ok();
    }
}
