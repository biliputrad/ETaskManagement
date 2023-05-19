namespace ETaskManagement.Application.HashingPassword;

public interface IHashingPasswordService
{
    string HashingPassword(string password);
    bool CheckPassword(string hashPassword, string password);
}