using ErrorOr;
using ETaskManagement.Application.User;
using ETaskManagement.Domain.Common.PageList;

namespace ETaskManagement.Application.Task;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUserRepository _userRepository;

    public TaskService(ITaskRepository taskRepository, IUserRepository userRepository)
    {
        _taskRepository = taskRepository;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<Domain.Task.Task>> Create(Domain.Task.Task input)
    {
        if (input.PriorityLevel < 0 || input.PriorityLevel > 6)
            return Error.Failure("Priority Must be range 1 until 5");
        
        if (input.DueDate > DateTime.Now && input.IsFinished.Equals(false))
            input.Notification = "Over Due";
        else if (input.DueDate > DateTime.Now && input.IsFinished.Equals(true))
            input.Notification = "Complete";
        else if (input.IsFinished.Equals(true))
            input.Notification = "Complete";
        else
            input.Notification = "Due Soon";
        
        var result = await _taskRepository.Create(input);

        return result;
    }

    public async Task<ErrorOr<Domain.Task.Task>> Update(Domain.Task.Task input)
    {
        var task = await _taskRepository.GetById(input.Id);
        if (task is null)
            return Error.NotFound("Invalid Id Task");

        task.IsFinished = input.IsFinished;
        
        if (DateTime.Now > task.DueDate && task.IsFinished.Equals(false))
            task.Notification = "Over Due";
        else if (DateTime.Now > task.DueDate && task.IsFinished.Equals(true))
            task.Notification = "Complete";
        else if (task.IsFinished.Equals(true))
            task.Notification = "Complete";
        else
            task.Notification = "Due Soon";

        var result = await _taskRepository.Update(task);

        return result;
    }

    public async Task<ErrorOr<Domain.Task.Task>> GetById(Guid id)
    {
        var task = await _taskRepository.GetById(id);
        if (task is null)
            return Error.NotFound("Invalid Id Task");

        if (DateTime.Now > task.DueDate && task.IsFinished.Equals(false))
            task.Notification = "Over Due";
        else if (DateTime.Now > task.DueDate && task.IsFinished.Equals(true))
            task.Notification = "Complete";
        else if (task.IsFinished.Equals(true))
            task.Notification = "Complete";
        else
            task.Notification = "Due Soon";
        
        await _taskRepository.Update(task);

        return task;
    }

    public async Task<ErrorOr<PagedList<Domain.Task.Task>>> GetByUserId(DateTime? createFrom, DateTime? createTo, DateTime? dateFrom, DateTime? dateTo, int? priorityFrom,
        int? priorityTo, bool? isFinish, Guid id, bool? orderByDueDate, bool? orderByPriority, int? limit, int? page)
    {
        if (priorityFrom < 0 && priorityTo > priorityFrom && priorityTo < 6)
            return Error.Failure("Priority Must be range 1 until 5 and Priority From Must Be smaller than priority To");
        var user = await _userRepository.GetById(id);
        if (user is null)
            return Error.Failure("Invalid user");

        var result = _taskRepository.GetByUserId(createFrom, createTo, dateFrom, dateTo, priorityFrom, priorityTo,
            isFinish, id, orderByDueDate, orderByPriority);

        foreach (var task in result)
        {
            if (DateTime.Now > task.DueDate && task.IsFinished.Equals(false))
                task.Notification = "Over Due";
            else if (DateTime.Now > task.DueDate && task.IsFinished.Equals(true))
                task.Notification = "Complete";
            else if (task.IsFinished.Equals(true))
                task.Notification = "Complete";
            else
                task.Notification = "Due Soon";
            
            await _taskRepository.Update(task);
        }
        
        return PagedList<Domain.Task.Task>.Create(result, page is null ? 0 : page.Value, limit is null ? 0 : limit.Value);
    }
}