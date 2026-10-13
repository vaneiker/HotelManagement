using HotelManagement.Application.DTOs.Roles;
using HotelManagement.Application.Interfaces;

namespace HotelManagement.Application.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _repository;

    public RoleService(IRoleRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<RoleDto>> GetAllAsync(int companyId)
    {
        return await _repository.GetAllAsync(companyId);
    }

    public async Task<RoleDto?> GetByIdAsync(
        int companyId,
        int roleId)
    {
        return await _repository.GetByIdAsync(
            companyId,
            roleId);
    }

    public async Task<int> CreateAsync(
        CreateRoleRequest request)
    {
        return await _repository.CreateAsync(request);
    }

    public async Task<bool> UpdateAsync(
        int companyId,
        int roleId,
        UpdateRoleRequest request)
    {
        return await _repository.UpdateAsync(
            companyId,
            roleId,
            request);
    }

    public async Task<bool> DeleteAsync(
        int companyId,
        int roleId)
    {
        return await _repository.DeleteAsync(
            companyId,
            roleId);
    }
}