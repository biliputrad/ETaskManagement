namespace ETaskManagement.Contract.Common.Response;

public record ResponseSuccess(
    int StatusCode,
    string Message,
    Object Data,
    MetaData? Metadata
    );