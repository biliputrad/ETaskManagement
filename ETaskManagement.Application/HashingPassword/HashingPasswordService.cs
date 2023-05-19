using System.Security.Cryptography;
using ETaskManagement.Domain.PasswordOption;
using Microsoft.Extensions.Options;

namespace ETaskManagement.Application.HashingPassword;

public class HashingPasswordService :IHashingPasswordService
{
    private readonly PasswordOption _options;

    public HashingPasswordService(IOptions<PasswordOption> options)
    {
        _options = options.Value;
    }
    
    public bool CheckPassword(string hash, string password)
    {
        var parts = hash.Split('.');
        if (parts.Length != 3)
        {
            throw new FormatException("Unexpected hash format");
        }

        var iterations = Convert.ToInt32(parts[0]);
        var salt = Convert.FromBase64String(parts[1]);
        var key = Convert.FromBase64String(parts[2]);

        using (var algoritm = new Rfc2898DeriveBytes(
                   password,
                   salt,
                   iterations
               ))
        {
            var keyToCheck = algoritm.GetBytes(_options.KeySize);
            return keyToCheck.SequenceEqual(key);
        }
    }

    public string HashingPassword(string password)
    {
        using (var algoritm = new Rfc2898DeriveBytes(
                   password,
                   _options.SaltSize,
                   _options.Iterations
               ))
        {
            var key = Convert.ToBase64String(algoritm.GetBytes(_options.KeySize));
            var salt = Convert.ToBase64String(algoritm.Salt);

            return $"{_options.Iterations}.{salt}.{key}";
        }
    }
}