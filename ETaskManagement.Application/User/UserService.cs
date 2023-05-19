using ErrorOr;
using ETaskManagement.Application.HashingPassword;

namespace ETaskManagement.Application.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IHashingPasswordService _hashingPasswordService;

    public UserService(IUserRepository userRepository, IHashingPasswordService hashingPasswordService)
    {
        _userRepository = userRepository;
        _hashingPasswordService = hashingPasswordService;
    }

    public async Task<ErrorOr<Domain.User.User>> Create(Domain.User.User input)
    {
        var user = _userRepository.GetByEmail(input.Email);
        if (user.Result is not null)
            return Error.Conflict("User email already exist");
        var password = _hashingPasswordService.HashingPassword(input.Password);
        input.Password = password;
        
        var result = await _userRepository.Create(input);
        return result;
    }

    public async Task<ErrorOr<Domain.User.User>> GetById(Guid id)
    {
        var result = _userRepository.GetById(id);
        if (result.Result is null)
            return Error.NotFound("Invalid user Id");

        return result.Result;
    }

    public async Task<ErrorOr<Domain.User.User>> Update(Domain.User.User input)
    {
        var user = await _userRepository.GetById(input.Id);
        if (user is null)
            return Error.NotFound("Invalid user Id");

        user.FirstName = input.FirstName;
        user.LastName = input.LastName;
        user.Password = input.Password;
        user.Division = input.Division;
        user.Roles = input.Roles;
        var result = await _userRepository.Update(user);

        return result;
    }
}