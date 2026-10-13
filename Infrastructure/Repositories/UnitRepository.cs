using Dapper;
using HotelManagement.Application.Interfaces;
using HotelManagement.Domain.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HotelManagement.Infrastructure.Repositories;

public class UnitRepository : IUnitRepository
{
    private readonly string _connectionString;

    public UnitRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    private IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }

    public async Task<IEnumerable<Unit>> GetAllAsync(int propertyId)
    {
        using var connection = CreateConnection();

        return await connection.QueryAsync<Unit>(
            "[Property].[UNIT_GET_ALL]",
            new
            {
                PropertyId = propertyId
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<Unit?> GetByIdAsync(
        int propertyId,
        int unitId)
    {
        using var connection = CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<Unit>(
            "[Property].[UNIT_GET_BY_ID]",
            new
            {
                PropertyId = propertyId,
                UnitId = unitId
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<int> CreateAsync(Unit unit)
    {
        using var connection = CreateConnection();

        return await connection.ExecuteScalarAsync<int>(
            "[Property].[UNIT_CREATE]",
            new
            {
                unit.PropertyId,
                unit.UnitTypeId,
                unit.Code,
                unit.Name,
                unit.FloorNumber,
                unit.Description
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<bool> UpdateAsync(Unit unit)
    {
        using var connection = CreateConnection();

        var rowsAffected = await connection.ExecuteScalarAsync<int>(
            "[Property].[UNIT_UPDATE]",
            new
            {
                unit.PropertyId,
                unit.UnitId,
                unit.UnitTypeId,
                unit.Code,
                unit.Name,
                unit.FloorNumber,
                unit.Description,
                unit.IsActive
            },
            commandType: CommandType.StoredProcedure);

        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(
        int propertyId,
        int unitId)
    {
        using var connection = CreateConnection();

        var rowsAffected = await connection.ExecuteScalarAsync<int>(
            "[Property].[UNIT_DELETE]",
            new
            {
                PropertyId = propertyId,
                UnitId = unitId
            },
            commandType: CommandType.StoredProcedure);

        return rowsAffected > 0;
    }
}