using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementApi.Features.Users.Domain.Entities;

namespace TaskManagementApi.Features.Users.Infrastructure.Persistence
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.CreatedAtUtc)
                .IsRequired();

            builder.Property(u => u.UpdatedAtUtc)
                .IsRequired();

            builder.HasIndex(u => u.Username)
                .IsUnique();
        }
    }
}