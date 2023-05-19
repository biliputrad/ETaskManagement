using ErrorOr;

namespace ETaskManagement.Application.User;

public interface IUserService
{
    Task<ErrorOr<Domain.User.User>> Create(Domain.User.User input);
    Task<ErrorOr<Domain.User.User>> GetById(Guid id);
    Task<ErrorOr<Domain.User.User>> Update(Domain.User.User input);
}