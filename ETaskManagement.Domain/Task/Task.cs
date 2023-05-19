using ETaskManagement.Domain.Common.Base;

namespace ETaskManagement.Domain.Task;

public class Task : Base
{
    public string TaskName { get; set; }
    public DateTime DueDate { get; set; }
    public int PriorityLevel { get; set; }
    public bool IsFinished { get; set; }
    public Guid UserId { get; set; }
    public User.User User { get; set; }
    
    public string Notification { get; set; }
}