namespace ETaskManagement.Contract.Login.Request;

public record LoginRequest(
    string Email,
    string Password);

