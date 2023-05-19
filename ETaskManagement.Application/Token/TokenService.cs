using System.Text;
using ErrorOr;
using ETaskManagement.Application.User;
using ETaskManagement.Domain.TokenOptions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ETaskManagement.Application.HashingPassword;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ETaskManagement.Application.Token;

public class TokenService : ITokenService
{
    private readonly IUserRepository _userRepository;
    private readonly TokenOptions _options;
    private readonly IHashingPasswordService _hashingPasswordService;
    
    public TokenService(IUserRepository userRepository, IOptions<TokenOptions> options, IHashingPasswordService hashingPasswordService)
    {
        _userRepository = userRepository;
        _options = options.Value;
        _hashingPasswordService = hashingPasswordService;
    }

    public async Task<ErrorOr<Domain.Token.Token>>  GenerateTokenUser(Domain.User.User input)
    {
        var user = _userRepository.GetByEmail(input.Email);
        if (user.Result is null)
        {
            return Error.Failure("Invalid email or password");
        }

        var check = _hashingPasswordService.CheckPassword(user.Result.Password, input.Password);
        if (!check)
        {
            return Error.Failure("Invalid email or password");
        }

        var secretKey = _options.SecretKey;
        var expiresMinutes = _options.ExpiresMinutes;

        var signedCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Result.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Result.Email)
        };

        var expiredAt = DateTime.Now.AddMinutes(_options.ExpiresMinutes);

        var securityToken = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            expires: expiredAt,
            claims : claims,
            signingCredentials: signedCredentials
        );

        var result = new Domain.Token.Token(
            
        );

        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

        return new Domain.Token.Token
        {
            ExpiredDate = expiredAt,
            TokenValue = token
        };
    }
}