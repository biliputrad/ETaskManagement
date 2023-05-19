using ETaskManagement.Application.HashingPassword;
using ETaskManagement.Application.Task;
using ETaskManagement.Application.Token;
using ETaskManagement.Application.User;
using ETaskManagement.Application.UserIdentity;
using Microsoft.Extensions.DependencyInjection;

namespace ETaskManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // add services to container
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IHashingPasswordService, HashingPasswordService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserIdentityService, UserIdentityService>();
        services.AddScoped<ITaskService, TaskService>();

        return services;
    }
}