using ETaskManagement.Contract.Common.Response;

namespace ETaskManagement.Contract.Common.Pagination;

public record PaginatioResponse(
    object Data,
    MetaData Metadata);