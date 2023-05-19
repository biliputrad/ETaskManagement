namespace ETaskManagement.Domain.PasswordOption;

public class PasswordOption
{
    public int SaltSize { get; set; }
    public int KeySize { get; set; }
    public int Iterations { get; set; }
}