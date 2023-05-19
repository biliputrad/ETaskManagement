namespace ETaskManagement.Application.User;

public interface IUserRepository
{
    Task<Domain.User.User> Create(Domain.User.User input);
    Task<Domain.User.User?> GetById(Guid id);
    Task<Domain.User.User> Update(Domain.User.User input);
    Task<Domain.User.User?> GetByEmail(string email);
}