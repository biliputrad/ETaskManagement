using ETaskManagement.Contract.Common.Filter;
namespace ETaskManagement.Contract.Task.Request;

public record CreateTask(
    string TaskName,
    DateTime DueDate,
    int PriorityLevel,
    bool IsFinished,
    Guid UserId);
    
public record UpdateTask(
    Guid Id,
    bool IsFinished);
    
public class TaskFilter : Filter
{
    public DateTime? CreateFrom { get; set; } = null;
    public DateTime? CreateTo { get; set; } = null;
    public DateTime? DateFrom { get; set; } = null;
    public DateTime? DateTo { get; set; } = null;
    public int? PriorityFrom { get; set; } = 0;
    public int? PriorityTo { get; set; } = 0;
    public bool? IsFinish { get; set; } = null;
    public Guid Id { get; set; }
    public bool? OrderByDueDate { get; set; } = null;
    public bool? OrderByPriority { get; set; } = null;
}