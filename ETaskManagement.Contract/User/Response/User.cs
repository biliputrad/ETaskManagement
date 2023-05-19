namespace ETaskManagement.Contract.User.Response;

public record UserResponse(
    Guid Id,
    string Email,
    string FirstName,
    string? LastName,
    string Division,
    string Roles);