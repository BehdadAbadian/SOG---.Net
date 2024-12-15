using Microsoft.EntityFrameworkCore;
using Security.Domain.User;
using Security.Infrastructure.Database.Configuration;

namespace Security.Infrastructure.Database;

public class SecurityContext : DbContext
{
    public DbSet<User> users {  get; set; }
    public SecurityContext(DbContextOptions<SecurityContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = typeof(UserConfiguration).Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        base.OnModelCreating(modelBuilder);
    }
}
