using HotelManagement.Application.DTOs.Roles;

namespace HotelManagement.Application.Interfaces;

public interface IRoleService
{
    Task<IEnumerable<RoleDto>> GetAllAsync(int companyId);

    Task<RoleDto?> GetByIdAsync(
        int companyId,
        int roleId);

    Task<int> CreateAsync(
        CreateRoleRequest request);

    Task<bool> UpdateAsync(
        int companyId,
        int roleId,
        UpdateRoleRequest request);

    Task<bool> DeleteAsync(
        int companyId,
        int roleId);
}