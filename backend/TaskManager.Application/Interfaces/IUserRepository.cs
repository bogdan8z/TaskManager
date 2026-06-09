using TaskManager.Domain.Entities;

namespace TaskManager.Application.Interfaces;
public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(Guid id);
    Task AddAsync(User user);
}