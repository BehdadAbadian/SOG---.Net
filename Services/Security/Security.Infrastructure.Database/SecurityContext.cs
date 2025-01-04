using Microsoft.EntityFrameworkCore;
using Security.Domain.Security;
using Security.Domain.User;
using Security.Infrastructure.Database.Configuration;

namespace Security.Infrastructure.Database;

public class SecurityContext : DbContext
{
    public DbSet<User> users {  get; set; }
    public DbSet<Permission> permissions { get; set; }
    public DbSet<Role> roles { get; set; }
    public DbSet<RolePermission> rolesPermission { get; set; }
    public DbSet<RoleUser> roleUsers { get; set; }
    public SecurityContext(DbContextOptions<SecurityContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = typeof(UserConfiguration).Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        base.OnModelCreating(modelBuilder);
    }
}
