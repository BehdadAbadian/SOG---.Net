using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notification.Domain.Email;

namespace Notification.Infrastrucutre.Database.Configuration;

public class EmailConfiguration : IEntityTypeConfiguration<Email>
{
    public void Configure(EntityTypeBuilder<Email> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Sender).IsRequired();
        builder.Property(x => x.EmailAddress).IsRequired();
        builder.Property(x => x.Subject).IsRequired();
        builder.Property(x => x.Body).IsRequired();
        builder.Property(x => x.SendDate).IsRequired();
        builder.Property(x => x.CreationDate).IsRequired();
        builder.Property(x => x.EmailStatus).IsRequired();

    }
}
