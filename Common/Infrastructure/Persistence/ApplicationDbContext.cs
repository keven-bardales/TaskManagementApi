using Microsoft.EntityFrameworkCore;
using TaskEntity = TaskManagementApi.Features.Tasks.Domain.Entities.TaskEntity;

namespace TaskManagementApi.Common.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TaskEntity> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Task entity configuration
            modelBuilder.Entity<TaskEntity>(entity =>
            {
                entity.HasKey(t => t.Id);

                entity.Property(t => t.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(t => t.Description)
                    .HasMaxLength(1000);

                entity.Property(t => t.IsCompleted)
                    .IsRequired();

                entity.Property(t => t.CreatedAtUtc)
                    .IsRequired();

                entity.Property(t => t.UpdatedAtUtc)
                    .IsRequired();

                entity.HasIndex(t => t.IsCompleted);
                entity.HasIndex(t => t.DueDate);
            });
        }
    }
}