using EKO.ToDoApp.AppLogic.Services.Contracts;
using EKO.ToDoApp.Infrastructure.Storage;
using EKO.ToDoApp.Shared.Entities;
using EKO.ToDoApp.Shared.Exceptions;
using EKO.ToDoApp.Shared.Requests.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EKO.ToDoApp.AppLogic.Services;

public sealed class TaskService : ITaskService
{
    private readonly IConfiguration _configuration;

    public TaskService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task Complete(CompleteToDoTaskRequest request)
    {
        using ToDoDbContext ctx = new(_configuration);

        var item = await ctx.ToDos.SingleOrDefaultAsync(x => x.Id == request.Id);

        if (item is null)
        {
            throw new ItemNotFoundException($"Item with Id {request.Id} was not found.");
        }

        if (item.UserId != request.UserId)
        {
            throw new ItemNotFoundException($"Item with Id {request.Id} does not belong to user with Id {request.UserId}.");
        }

        item.IsCompleted = true;

        await ctx.SaveChangesAsync();
    }

    public async Task Create(CreateToDoTaskRequest request)
    {
        var item = new ToDoTaskEntity
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            IsCompleted = request.IsCompleted,
            DueBy = request.DueBy,
            TaskListId = request.AssociatedList,
            Urgency = request.UrgencyLevel,
            UserId = request.UserId
        };

        using ToDoDbContext ctx = new(_configuration);

        _ = await ctx.ToDos.AddAsync(item);

        await ctx.SaveChangesAsync();
    }

    public async Task Delete(DeleteToDoTaskRequest request)
    {
        using ToDoDbContext ctx = new(_configuration);

        var item = await ctx.ToDos.SingleOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId );

        if (item is not null)
        {
            if (item.UserId != request.UserId)
            {
                throw new ItemNotFoundException($"Item with Id {request.Id} does not belong to user with Id {request.UserId}.");
            }

            _ = ctx.ToDos.Remove(item);

            await ctx.SaveChangesAsync();

            return;
        }

        throw new ItemNotFoundException($"Item with Id {request.Id} was not found.");
    }

    public async Task<IEnumerable<ToDoTaskEntity>> GetAll()
    {
        using ToDoDbContext ctx = new(_configuration);

        return await ctx.ToDos.ToListAsync();
    }

    public async Task<IEnumerable<ToDoTaskEntity>> GetAllFromList(GetAllTasksListRequest request)
    {
        using ToDoDbContext ctx = new(_configuration);

        return await ctx.ToDos.Where(x => x.TaskListId == request.Id && x.UserId == request.UserId).ToListAsync();
    }

    public Task<ToDoTaskEntity?> GetById(Guid id, Guid userId)
    {
        using ToDoDbContext ctx = new(_configuration);

        var item = ctx.ToDos.SingleOrDefaultAsync(x => x.Id == id && x.UserId == userId);

        if (item is null)
        {
            throw new ItemNotFoundException($"Item with Id {id} was not found.");
        }

        return item;
    }

    public async Task Update(UpdateToDoTaskRequest request)
    {
        using ToDoDbContext ctx = new(_configuration);

        var item = await ctx.ToDos.SingleOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

        if (item is null)
        {
            throw new ItemNotFoundException($"Item with Id {request.Id} was not found.");
        }

        item.Title = request.Title;
        item.Description = request.Description;
        item.DueBy = request.DueBy;
        item.Urgency = request.Urgency;
        item.IsCompleted = false;
        item.TaskListId = request.TaskListId;

        await ctx.SaveChangesAsync();
    }
}
