using HotelManagement.Application.DTOs.Users;
using HotelManagement.Application.Interfaces;
using HotelManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace HotelManagement.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly PasswordHasher<User> _passwordHasher;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _passwordHasher = new PasswordHasher<User>();
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync(
        int companyId)
    {
        var users =
            await _userRepository.GetAllAsync(companyId);

        return users.Select(MapToDto);
    }

    public async Task<UserDto?> GetByIdAsync(
        int companyId,
        int userId)
    {
        var user =
            await _userRepository.GetByIdAsync(
                companyId,
                userId);

        if (user == null)
            return null;

        return MapToDto(user);
    }

    public async Task<int> CreateAsync(
        CreateUserRequest request)
    {
        var user = new User
        {
            CompanyId = request.CompanyId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Username = request.Username,
            Email = request.Email,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        user.PasswordHash =
            _passwordHasher.HashPassword(
                user,
                request.Password);

        return await _userRepository.CreateAsync(user);
    }

    public async Task<bool> UpdateAsync(
        int companyId,
        int userId,
        UpdateUserRequest request)
    {
        var user =
            await _userRepository.GetByIdAsync(
                companyId,
                userId);

        if (user == null)
            return false;

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Username = request.Username;
        user.Email = request.Email;
        user.IsActive = request.IsActive;
        user.UpdatedAt = DateTime.UtcNow;

        if (!string.IsNullOrWhiteSpace(request.Password))
        {
            user.PasswordHash =
                _passwordHasher.HashPassword(
                    user,
                    request.Password);
        }

        return await _userRepository.UpdateAsync(user);
    }

    public async Task<bool> DeleteAsync(
        int companyId,
        int userId)
    {
        return await _userRepository.DeleteAsync(
            companyId,
            userId);
    }

    private static UserDto MapToDto(User user)
    {
        return new UserDto
        {
            UserId = user.UserId,
            CompanyId = user.CompanyId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Username = user.Username,
            Email = user.Email,
            IsActive = user.IsActive
        };
    }
}