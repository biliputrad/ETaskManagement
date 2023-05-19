using ErrorOr;
using ETaskManagement.Domain.Common.PageList;

namespace ETaskManagement.Application.Task;
using Domain.Task;
public interface ITaskService
{
    Task<ErrorOr<Task>> Create(Task input);
    Task<ErrorOr<Task>> GetById(Guid id);
    Task<ErrorOr<Task>> Update(Task input);
    Task<ErrorOr<PagedList<Task>>> GetByUserId(DateTime? createFrom, DateTime? createTo, 
        DateTime? dateFrom, DateTime? dateTo, int? priorityFrom, int? priorityTo, 
        bool? isFinish, Guid id, bool? orderByDueDate, bool? orderByPriority, int? limit, int? page);
}