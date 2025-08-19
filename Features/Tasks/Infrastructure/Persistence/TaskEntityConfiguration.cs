using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementApi.Features.Tasks.Domain.Entities;

namespace TaskManagementApi.Features.Tasks.Infrastructure.Persistence
{
    public class TaskEntityConfiguration : IEntityTypeConfiguration<TaskEntity>
    {
        public void Configure(EntityTypeBuilder<TaskEntity> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.Description)
                .HasMaxLength(1000);

            builder.Property(t => t.IsCompleted)
                .IsRequired();

            builder.Property(t => t.CreatedAtUtc)
                .IsRequired();

            builder.Property(t => t.UpdatedAtUtc)
                .IsRequired();

            builder.HasIndex(t => t.IsCompleted);
            builder.HasIndex(t => t.DueDate);
        }
    }
}