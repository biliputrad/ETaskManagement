namespace ETaskManagement.Domain.Token;

public class Token
{
    public string TokenValue { get; set; }
    public DateTime ExpiredDate { get; set; }
}