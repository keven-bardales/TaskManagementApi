using System;
using System.Threading.Tasks;
using TaskManagementApi.Features.Users.Domain.Entities;

namespace TaskManagementApi.Features.Users.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByUsernameAsync(string username);
        Task<User> AddAsync(User user);
        Task<bool> ExistsAsync(Guid id);
        Task<bool> UsernameExistsAsync(string username);
    }
}