using ETaskManagement.Application.User;
using ETaskManagement.Domain.User;
using Microsoft.EntityFrameworkCore;

namespace ETaskManagement.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly ETaskManagementDbContext _dbContext;

    public UserRepository(ETaskManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> Create(User input)
    {
        await _dbContext.Users.AddAsync(input);
        await _dbContext.SaveChangesAsync();
        return input;
    }

    public async Task<User?> GetById(Guid id)
    {
        var user = await _dbContext.Users
            .Where(u => !u.DeletedAt.HasValue)
            .FirstOrDefaultAsync(u => u.Id == id);

        return user;
    }

    public async Task<User> Update(User input)
    {
        await _dbContext.SaveChangesAsync();
        return input;
    }
    
    public async Task<User?> GetByEmail(string email)
    {
        var user = await _dbContext.Users
            .Where(u => !u.DeletedAt.HasValue)
            .FirstOrDefaultAsync(u => u.Email == email);

        return user;
    }
}