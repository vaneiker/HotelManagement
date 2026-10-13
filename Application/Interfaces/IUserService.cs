using HotelManagement.Application.DTOs.Users;

namespace HotelManagement.Application.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllAsync(
        int companyId);

    Task<UserDto?> GetByIdAsync(
        int companyId,
        int userId);

    Task<int> CreateAsync(
        CreateUserRequest request);

    Task<bool> UpdateAsync(
        int companyId,
        int userId,
        UpdateUserRequest request);

    Task<bool> DeleteAsync(
        int companyId,
        int userId);
}