using ETaskManagement.Domain.Common.Base;

namespace ETaskManagement.Domain.User;

public class User : Base
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string Division { get; set; }
    public string Roles { get; set; }
}