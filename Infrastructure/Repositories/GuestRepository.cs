using Dapper;
using HotelManagement.Application.Interfaces;
using HotelManagement.Domain.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HotelManagement.Infrastructure.Repositories;

public class GuestRepository : IGuestRepository
{
    private readonly string _connectionString;

    public GuestRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    private IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }

    public async Task<IEnumerable<Guest>> GetAllAsync(int companyId)
    {
        using var connection = CreateConnection();

        return await connection.QueryAsync<Guest>(
            "[Guest].[GET_ALL]",
            new
            {
                CompanyId = companyId
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<Guest?> GetByIdAsync(
        int companyId,
        long guestId)
    {
        using var connection = CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<Guest>(
            "[Guest].[GET_BY_ID]",
            new
            {
                CompanyId = companyId,
                GuestId = guestId
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<long> CreateAsync(Guest guest)
    {
        using var connection = CreateConnection();

        return await connection.ExecuteScalarAsync<long>(
            "[Guest].[CREATE]",
            new
            {
                guest.CompanyId,
                guest.FirstName,
                guest.LastName,
                guest.DocumentType,
                guest.DocumentNumber,
                guest.Email,
                guest.Phone,
                guest.Nationality,
                guest.Address,
                guest.City,
                guest.CountryCode,
                guest.Notes
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<bool> UpdateAsync(Guest guest)
    {
        using var connection = CreateConnection();

        var rowsAffected =
            await connection.ExecuteScalarAsync<int>(
                "[Guest].[UPDATE]",
                new
                {
                    guest.CompanyId,
                    guest.GuestId,
                    guest.FirstName,
                    guest.LastName,
                    guest.DocumentType,
                    guest.DocumentNumber,
                    guest.Email,
                    guest.Phone,
                    guest.Nationality,
                    guest.Address,
                    guest.City,
                    guest.CountryCode,
                    guest.Notes,
                    guest.IsActive
                },
                commandType: CommandType.StoredProcedure);

        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(
        int companyId,
        long guestId)
    {
        using var connection = CreateConnection();

        var rowsAffected =
            await connection.ExecuteScalarAsync<int>(
                "[Guest].[DELETE]",
                new
                {
                    CompanyId = companyId,
                    GuestId = guestId
                },
                commandType: CommandType.StoredProcedure);

        return rowsAffected > 0;
    }
}