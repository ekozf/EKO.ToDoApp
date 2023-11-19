using EKO.ToDoApp.AppLogic.Services.Contracts;
using EKO.ToDoApp.Infrastructure.Storage;
using EKO.ToDoApp.Shared.Entities;
using EKO.ToDoApp.Shared.Exceptions;
using EKO.ToDoApp.Shared.Requests.TaskLists;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EKO.ToDoApp.AppLogic.Services;

public sealed class TaskListService : ITaskListService
{
    private readonly IConfiguration _configuration;

    public TaskListService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task AddTask(AddTaskToListRequest request)
    {
        using ToDoDbContext ctx = new(_configuration);

        var taskList = await ctx.ToDoLists.FindAsync(request.TaskListId);

        if (taskList is null)
        {
            throw new ItemNotFoundException($"Task list with Id {request.TaskListId} was not found.");
        }

        if (taskList.UserId != request.UserId)
        {
            throw new ItemNotFoundException($"Task list with Id {request.TaskListId} does not belong to user with Id {request.UserId}.");
        }

        var task = await ctx.ToDos.FindAsync(request.TaskId);

        if (task is null)
        {
            throw new ItemNotFoundException($"Task with Id {request.TaskId} was not found.");
        }

        task.TaskListId = taskList.Id;

        await ctx.SaveChangesAsync();
    }

    public async Task Create(CreateTaskListRequest request)
    {
        var taskList = new TaskList
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            UserId = request.UserId
        };

        using ToDoDbContext ctx = new(_configuration);

        await ctx.ToDoLists.AddAsync(taskList);

        await ctx.SaveChangesAsync();
    }

    public async Task Delete(DeleteTaskListRequest request)
    {
        using ToDoDbContext ctx = new(_configuration);

        var taskList = await ctx.ToDoLists.FindAsync(request.Id);

        if (taskList is null)
        {
            throw new ItemNotFoundException($"Task list with Id {request.Id} was not found.");
        }

        if (taskList.UserId != request.UserId)
        {
            throw new ItemNotFoundException($"Task list with Id {request.Id} does not belong to user with Id {request.UserId}.");
        }

        _ = ctx.ToDoLists.Remove(taskList);

        await ctx.SaveChangesAsync();
    }

    public async Task<IEnumerable<TaskList>> GetAll(Guid userId)
    {
        using ToDoDbContext ctx = new(_configuration);

        return await ctx.ToDoLists.Where(x => x.UserId == userId).ToListAsync();
    }

    public async Task<TaskList> GetById(Guid id, Guid userId)
    {
        using ToDoDbContext ctx = new(_configuration);

        var list = await ctx.ToDoLists.FindAsync(id);

        if (list is null)
        {
            throw new ItemNotFoundException($"Task list with Id {id} was not found.");
        }

        if (list.UserId != userId)
        {
            throw new ItemNotFoundException($"Task list with Id {id} does not belong to user with Id {id}.");
        }

        return list;
    }

    public async Task Update(UpdateTaskListRequest request)
    {
        using ToDoDbContext ctx = new(_configuration);

        var list = ctx.ToDoLists.Find(request.Id);

        if (list is null)
        {
            throw new ItemNotFoundException($"Task list with Id {request.Id} was not found.");
        }

        if (list.UserId != request.UserId)
        {
            throw new ItemNotFoundException($"Task list with Id {request.Id} does not belong to user with Id {request.UserId}.");
        }

        list.Title = request.Title;

        await ctx.SaveChangesAsync();
    }
}
