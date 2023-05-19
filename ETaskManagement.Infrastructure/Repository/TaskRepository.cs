using ETaskManagement.Application.Task;
using Microsoft.EntityFrameworkCore;
using Task = ETaskManagement.Domain.Task.Task;

namespace ETaskManagement.Infrastructure.Repository;

public class TaskRepository : ITaskRepository
{
    private readonly ETaskManagementDbContext _dbContext;

    public TaskRepository(ETaskManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Task> Create(Task input)
    {
        await _dbContext.Tasks.AddAsync(input);
        await _dbContext.SaveChangesAsync();
        return input;
    }

    public async Task<Task?> GetById(Guid id)
    {
        var user = await _dbContext.Tasks
            .Include(u => u.User)
            .Where(u => !u.DeletedAt.HasValue)
            .FirstOrDefaultAsync(u => u.Id == id);

        return user;
    }

    public async Task<Task> Update(Task input)
    {
        await _dbContext.SaveChangesAsync();
        return input;
    }
    
    public List<Task> GetByUserId(DateTime? createFrom, DateTime? createTo, 
        DateTime? dateFrom, DateTime? dateTo, int? priorityFrom, int? priorityTo,  
        bool? isFinish, Guid id, bool? orderByDueDate, bool? orderByPriority)
    {
        IQueryable<Task> taskIQ = from i in _dbContext.Tasks select i;

        if (dateFrom > DateTime.MinValue && dateTo > DateTime.MinValue)
            taskIQ = taskIQ.Where(x => x.DueDate >= dateFrom && x.DueDate <= dateTo);

        if (createFrom > DateTime.MinValue && dateTo > DateTime.MinValue)
            taskIQ = taskIQ.Where(x => x.CreatedAt >= createFrom && x.CreatedAt <= createTo);

        if (priorityFrom > 0 && priorityFrom < 6 && priorityTo > 0 && priorityTo < 6)
            taskIQ = taskIQ.Where(x => x.PriorityLevel >= priorityFrom && x.PriorityLevel <= priorityTo);

        if (isFinish.HasValue)
            taskIQ = taskIQ.Where(x => x.IsFinished == isFinish);

        taskIQ = taskIQ.Where(x => x.UserId == id);
        
        if (orderByDueDate.HasValue && orderByDueDate.Value)
        {
            taskIQ = taskIQ.OrderByDescending(x => x.DueDate);
        }
        else if (orderByPriority.HasValue && orderByPriority.Value)
        {
            taskIQ = taskIQ.OrderByDescending(x => x.PriorityLevel);
        }
        else
        {
            taskIQ = taskIQ.OrderByDescending(x => x.CreatedAt);
        }
        
        var tasks = taskIQ
            .AsNoTracking().
            ToList();
        
        return tasks;
    }
}