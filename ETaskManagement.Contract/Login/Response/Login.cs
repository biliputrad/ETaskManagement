namespace ETaskManagement.Contract.Login.Response;

public record LoginResponse(
    string TokenValue,
    DateTime ExpiredDate);