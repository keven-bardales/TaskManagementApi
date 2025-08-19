using Microsoft.EntityFrameworkCore;
using TaskEntity = TaskManagementApi.Features.Tasks.Domain.Entities.TaskEntity;
using TaskManagementApi.Features.Users.Domain.Entities;
using TaskManagementApi.Features.Tasks.Infrastructure.Persistence;
using TaskManagementApi.Features.Users.Infrastructure.Persistence;

namespace TaskManagementApi.Common.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply entity configurations from each feature
            modelBuilder.ApplyConfiguration(new TaskEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        }
    }
}