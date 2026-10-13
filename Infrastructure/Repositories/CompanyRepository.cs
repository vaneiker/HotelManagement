using Dapper;
using HotelManagement.Application.Interfaces;
using HotelManagement.Domain.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HotelManagement.Infrastructure.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly string _connectionString;

    public CompanyRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    private IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }

    public async Task<IEnumerable<Company>> GetAllAsync()
    {
        using var connection = CreateConnection();

        return await connection.QueryAsync<Company>(
            "[Security].[COMPANY_GET_ALL]",
            commandType: CommandType.StoredProcedure);
    }

    public async Task<Company?> GetByIdAsync(int companyId)
    {
        using var connection = CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<Company>(
            "[Security].[COMPANY_GET_BY_ID]",
            new
            {
                CompanyId = companyId
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<int> CreateAsync(Company company)
    {
        using var connection = CreateConnection();

        return await connection.ExecuteScalarAsync<int>(
            "[Security].[COMPANY_CREATE]",
            new
            {
                company.Name,
                company.LegalName,
                company.Rnc,
                company.Email,
                company.Phone,
                company.Address,
                company.CountryCode,
                company.CurrencyCode
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<bool> UpdateAsync(Company company)
    {
        using var connection = CreateConnection();

        var rowsAffected =
            await connection.ExecuteScalarAsync<int>(
                "[Security].[COMPANY_UPDATE]",
                new
                {
                    company.CompanyId,
                    company.Name,
                    company.LegalName,
                    company.Rnc,
                    company.Email,
                    company.Phone,
                    company.Address,
                    company.CountryCode,
                    company.CurrencyCode,
                    company.IsActive
                },
                commandType: CommandType.StoredProcedure);

        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(int companyId)
    {
        using var connection = CreateConnection();

        var rowsAffected =
            await connection.ExecuteScalarAsync<int>(
                "[Security].[COMPANY_DELETE]",
                new
                {
                    CompanyId = companyId
                },
                commandType: CommandType.StoredProcedure);

        return rowsAffected > 0;
    }
}