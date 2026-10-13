using Dapper;
using HotelManagement.Application.DTOs.Roles;
using HotelManagement.Application.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HotelManagement.Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly string _connectionString;

    public RoleRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<RoleDto>> GetAllAsync(int companyId)
    {
        using var connection = new SqlConnection(_connectionString);

        return await connection.QueryAsync<RoleDto>(
            "[Security].[ROLE_GET_ALL]",
            new
            {
                CompanyId = companyId
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<RoleDto?> GetByIdAsync(
        int companyId,
        int roleId)
    {
        using var connection = new SqlConnection(_connectionString);

        return await connection.QueryFirstOrDefaultAsync<RoleDto>(
            "[Security].[ROLE_GET_BY_ID]",
            new
            {
                CompanyId = companyId,
                RoleId = roleId
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<int> CreateAsync(
        CreateRoleRequest request)
    {
        using var connection = new SqlConnection(_connectionString);

        return await connection.QuerySingleAsync<int>(
            "[Security].[ROLE_CREATE]",
            new
            {
                request.CompanyId,
                request.Name,
                request.Description
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<bool> UpdateAsync(
        int companyId,
        int roleId,
        UpdateRoleRequest request)
    {
        using var connection = new SqlConnection(_connectionString);

        var rowsAffected = await connection.QuerySingleAsync<int>(
            "[Security].[ROLE_UPDATE]",
            new
            {
                CompanyId = companyId,
                RoleId = roleId,
                request.Name,
                request.Description,
                request.IsActive
            },
            commandType: CommandType.StoredProcedure);

        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(
        int companyId,
        int roleId)
    {
        using var connection = new SqlConnection(_connectionString);

        var rowsAffected = await connection.QuerySingleAsync<int>(
            "[Security].[ROLE_DELETE]",
            new
            {
                CompanyId = companyId,
                RoleId = roleId
            },
            commandType: CommandType.StoredProcedure);

        return rowsAffected > 0;
    }
}