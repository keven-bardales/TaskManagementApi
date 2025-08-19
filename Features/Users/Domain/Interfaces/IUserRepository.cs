using System;
using System.Threading.Tasks;
using TaskManagementApi.Features.Users.Domain.Entities;

namespace TaskManagementApi.Features.Users.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity?> GetByIdAsync(Guid id);
        Task<UserEntity?> GetByUsernameAsync(string username);
        Task<UserEntity> AddAsync(UserEntity user);
        Task<bool> ExistsAsync(Guid id);
        Task<bool> UsernameExistsAsync(string username);
    }
}