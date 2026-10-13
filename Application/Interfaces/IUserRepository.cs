using HotelManagement.Domain.Entities;

namespace HotelManagement.Application.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync(int companyId);

    Task<User?> GetByIdAsync(
        int companyId,
        int userId);

    Task<User?> GetByUsernameAsync(
        string username);

    Task<int> CreateAsync(User user);

    Task<bool> UpdateAsync(User user);

    Task<bool> DeleteAsync(
        int companyId,
        int userId);
}