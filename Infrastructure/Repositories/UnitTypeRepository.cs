using Dapper;
using HotelManagement.Application.Interfaces;
using HotelManagement.Domain.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HotelManagement.Infrastructure.Repositories;

public class UnitTypeRepository : IUnitTypeRepository
{
    private readonly string _connectionString;

    public UnitTypeRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    private IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }

    public async Task<IEnumerable<UnitType>> GetAllAsync(int propertyId)
    {
        using var connection = CreateConnection();

        return await connection.QueryAsync<UnitType>(
            "[Property].[UNIT_TYPE_GET_ALL]",
            new
            {
                PropertyId = propertyId
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<UnitType?> GetByIdAsync(
        int propertyId,
        int unitTypeId)
    {
        using var connection = CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<UnitType>(
            "[Property].[UNIT_TYPE_GET_BY_ID]",
            new
            {
                PropertyId = propertyId,
                UnitTypeId = unitTypeId
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<int> CreateAsync(UnitType unitType)
    {
        using var connection = CreateConnection();

        return await connection.ExecuteScalarAsync<int>(
            "[Property].[UNIT_TYPE_CREATE]",
            new
            {
                unitType.PropertyId,
                unitType.Name,
                unitType.Description,
                unitType.MaxAdults,
                unitType.MaxChildren,
                unitType.BaseCapacity
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<bool> UpdateAsync(UnitType unitType)
    {
        using var connection = CreateConnection();

        var rowsAffected = await connection.ExecuteScalarAsync<int>(
            "[Property].[UNIT_TYPE_UPDATE]",
            new
            {
                unitType.PropertyId,
                unitType.UnitTypeId,
                unitType.Name,
                unitType.Description,
                unitType.MaxAdults,
                unitType.MaxChildren,
                unitType.BaseCapacity,
                unitType.IsActive
            },
            commandType: CommandType.StoredProcedure);

        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(
        int propertyId,
        int unitTypeId)
    {
        using var connection = CreateConnection();

        var rowsAffected = await connection.ExecuteScalarAsync<int>(
            "[Property].[UNIT_TYPE_DELETE]",
            new
            {
                PropertyId = propertyId,
                UnitTypeId = unitTypeId
            },
            commandType: CommandType.StoredProcedure);

        return rowsAffected > 0;
    }
}