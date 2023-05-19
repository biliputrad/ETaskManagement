using ETaskManagement.Contract.User.Response;

namespace ETaskManagement.Contract.Task.Response;

public record DetailTask(
    Guid Id,
    string TaskName,
    DateTime DueDate,
    int PriorityLevel,
    bool IsFinished,
    string Notification,
    UserResponse User);
    
public record ResponseTask(
    Guid Id,
    string TaskName,
    DateTime DueDate,
    int PriorityLevel,
    string Notification,
    bool IsFinished);