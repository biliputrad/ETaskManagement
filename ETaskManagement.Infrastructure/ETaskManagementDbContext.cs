using System.Security.Claims;
using ETaskManagement.Domain.Common.Base;
using ETaskManagement.Domain.User;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ETaskManagement.Infrastructure;

public class ETaskManagementDbContext : DbContext
{
    private readonly IHttpContextAccessor _contextAccessor;

    public ETaskManagementDbContext(
        DbContextOptions<ETaskManagementDbContext> options,
        IHttpContextAccessor contextAccessor) : base(options)
    {
        _contextAccessor = contextAccessor;
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Domain.Task.Task> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<Domain.Task.Task>()
            .HasOne(t => t.User)
            .WithMany().OnDelete(DeleteBehavior.Restrict).HasForeignKey(t => t.UserId);

    }


    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        // set base model field values such as CreateDate, ModifiedDate ...
        InitializeBaseFieldValues();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        // set base model field values such as CreateDate, ModifiedDate ...
        InitializeBaseFieldValues();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void InitializeBaseFieldValues()
    {
        var entries = ChangeTracker.Entries();
        var timestamp = DateTime.UtcNow;
        var uid = _getUid();

        foreach (var entry in entries)
        {
            if (entry.Entity is Base trackable)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        trackable.CreatedAt = timestamp;
                        trackable.UpdatedAt = timestamp;
                        trackable.CreatedBy = uid;
                        trackable.UpdatedBy = uid;
                        break;
                    case EntityState.Modified:
                        trackable.UpdatedAt = timestamp;
                        trackable.UpdatedBy = uid;
                        entry.Property("CreatedAt").IsModified = false;
                        entry.Property("CreatedBy").IsModified = false;
                        break;
                }
            }
        }
    }

    private Guid _getUid()
    {
        var uid = Guid.Empty;

        if (_contextAccessor.HttpContext == null) return uid;

        var enumerable = (_contextAccessor.HttpContext.User.Identity as ClaimsIdentity)?.Claims;
        if (enumerable == null) return uid;

        var claim = enumerable.FirstOrDefault(c => c.Type == Common.Constants.UserIdClaimIdentifier);
        if (claim != null) uid = Guid.Parse(claim.Value);

        return uid;
    }
}
