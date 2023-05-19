namespace ETaskManagement.Contract.User.Request;

public record CreateUser(
    string Email,
    string FirstName,
    string? LastName,
    string Password,
    string Division,
    string Roles);
    
public record UpdateUser(
    Guid Id,
    string FirstName,
    string? LastName,
    string Password,
    string Division,
    string Roles
    );