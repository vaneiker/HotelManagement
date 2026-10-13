using Dapper;
using HotelManagement.Application.Interfaces;
using HotelManagement.Domain.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HotelManagement.Infrastructure.Repositories;

public class PropertyRepository : IPropertyRepository
{
    private readonly string _connectionString;

    public PropertyRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    private IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }

    public async Task<IEnumerable<Property>> GetAllAsync(int companyId)
    {
        using var connection = CreateConnection();

        return await connection.QueryAsync<Property>(
            "[Property].[GET_ALL]",
            new
            {
                CompanyId = companyId
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<Property?> GetByIdAsync(
        int companyId,
        int propertyId)
    {
        using var connection = CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<Property>(
            "[Property].[GET_BY_ID]",
            new
            {
                CompanyId = companyId,
                PropertyId = propertyId
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<int> CreateAsync(Property property)
    {
        using var connection = CreateConnection();

        return await connection.ExecuteScalarAsync<int>(
            "[Property].[CREATE]",
            new
            {
                property.CompanyId,
                property.Name,
                property.PropertyType,
                property.Description,
                property.Address,
                property.City,
                property.Province,
                property.CountryCode,
                property.Phone,
                property.Email
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<bool> UpdateAsync(Property property)
    {
        using var connection = CreateConnection();

        var rowsAffected = await connection.ExecuteScalarAsync<int>(
            "[Property].[UPDATE]",
            new
            {
                property.CompanyId,
                property.PropertyId,
                property.Name,
                property.PropertyType,
                property.Description,
                property.Address,
                property.City,
                property.Province,
                property.CountryCode,
                property.Phone,
                property.Email,
                property.IsActive
            },
            commandType: CommandType.StoredProcedure);

        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(
        int companyId,
        int propertyId)
    {
        using var connection = CreateConnection();

        var rowsAffected = await connection.ExecuteScalarAsync<int>(
            "[Property].[DELETE]",
            new
            {
                CompanyId = companyId,
                PropertyId = propertyId
            },
            commandType: CommandType.StoredProcedure);

        return rowsAffected > 0;
    }
}