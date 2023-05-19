using ErrorOr;
namespace ETaskManagement.Application.Token;

public interface ITokenService
{
    Task<ErrorOr<Domain.Token.Token>> GenerateTokenUser(Domain.User.User input);
}