using Microsoft.EntityFrameworkCore;
using Notification.Domain.Email;
using Notification.Infrastrucutre.Database.Configuration;

namespace Notification.Infrastructure.Database
{
    public class NotificationContext : DbContext
    {
        public DbSet<Email> Emails { get; set; }
        public NotificationContext(DbContextOptions<NotificationContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(EmailConfiguration).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
