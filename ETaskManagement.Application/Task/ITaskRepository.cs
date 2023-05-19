namespace ETaskManagement.Application.Task;

public interface ITaskRepository
{
    Task<Domain.Task.Task> Create(Domain.Task.Task input);
    Task<Domain.Task.Task?> GetById(Guid id);
    Task<Domain.Task.Task> Update(Domain.Task.Task input);
    List<Domain.Task.Task> GetByUserId(DateTime? createFrom, DateTime? createTo, 
        DateTime? dateFrom, DateTime? dateTo, int? priorityFrom, int? priorityTo, 
        bool? isFinish, Guid id, bool? orderByDueDate, bool? orderByPriority);
}