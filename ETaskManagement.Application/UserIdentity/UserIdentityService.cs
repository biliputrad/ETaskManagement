using System.Security.Claims;
using ETaskManagement.Application.Common;
using Microsoft.AspNetCore.Http;

namespace ETaskManagement.Application.UserIdentity;

public class UserIdentityService : IUserIdentityService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public UserIdentityService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public Guid GetUserId()
    {
        var httpContext = _contextAccessor.HttpContext;
        var uid = Guid.Empty;
        
        if (httpContext == null) return uid;
        
        var enumerable = (httpContext.User.Identity as ClaimsIdentity)?.Claims;
        if (enumerable == null) return uid;
        
        var claim = enumerable.FirstOrDefault(c => c.Type == Constants.UserIdClaimIdentifier);
        if (claim != null) uid = Guid.Parse(claim.Value);

        return uid;
    }
}