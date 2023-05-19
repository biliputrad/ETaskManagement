using ETaskManagement.Application.Task;
using ETaskManagement.Application.User;
using ETaskManagement.Domain.PasswordOption;
using ETaskManagement.Domain.TokenOptions;
using ETaskManagement.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ETaskManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddDbContext<ETaskManagementDbContext>(options => 
            options.UseNpgsql(configuration.GetConnectionString("Default")));
        services.Configure<PasswordOption>(options => configuration.GetSection("PasswordOptions").Bind(options));
        services.Configure<TokenOptions>(options => configuration.GetSection("TokenOptions").Bind(options));

        return services;
    }
    
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        return services;
    }
}