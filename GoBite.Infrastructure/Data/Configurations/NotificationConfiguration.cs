using GoBite.Domain.NotificationُEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoBite.Infrastructure.Data.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            // Table
            builder.ToTable("Notifications");

            // Primary Key
            builder.HasKey(n => n.Id);

            // Properties
            builder.Property(n => n.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(n => n.Message)
                   .IsRequired()
                   .HasMaxLength(1000);

            builder.Property(n => n.NotificationType)
                   .IsRequired();

            builder.Property(n => n.NotificationTarget)
                   .IsRequired();

            builder.Property(n => n.IsRead)
                   .HasDefaultValue(false);

            builder.Property(n => n.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Indexes
            builder.HasIndex(n => n.UserId);

            builder.HasIndex(n => n.IsRead);

            builder.HasIndex(n => n.CreatedAt);

            builder.HasIndex(n => new
            {
                n.UserId,
                n.IsRead
            });

            // Relationships
            builder.HasOne(n => n.User)
                   .WithMany(u => u.Notifications)
                   .HasForeignKey(n => n.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
