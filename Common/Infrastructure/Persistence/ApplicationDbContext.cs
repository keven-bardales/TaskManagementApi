using Microsoft.EntityFrameworkCore;
using TaskEntity = TaskManagementApi.Features.Tasks.Domain.Entities.TaskEntity;
using TaskManagementApi.Features.Users.Domain.Entities;

namespace TaskManagementApi.Common.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<User> Users { get; set; }

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

            // User entity configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(u => u.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(u => u.CreatedAtUtc)
                    .IsRequired();

                entity.Property(u => u.UpdatedAtUtc)
                    .IsRequired();

                entity.HasIndex(u => u.Username)
                    .IsUnique();
            });
        }
    }
}