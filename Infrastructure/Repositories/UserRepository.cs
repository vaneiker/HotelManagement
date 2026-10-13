using Dapper;
using HotelManagement.Application.Interfaces;
using HotelManagement.Domain.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HotelManagement.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString;

    public UserRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    private IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }

    public async Task<IEnumerable<User>> GetAllAsync(
        int companyId)
    {
        using var connection = CreateConnection();

        return await connection.QueryAsync<User>(
            "[Security].[USER_GET_ALL]",
            new
            {
                CompanyId = companyId
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<User?> GetByIdAsync(
        int companyId,
        int userId)
    {
        using var connection = CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<User>(
            "[Security].[USER_GET_BY_ID]",
            new
            {
                CompanyId = companyId,
                UserId = userId
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<User?> GetByUsernameAsync(
        string username)
    {
        using var connection = CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<User>(
            "[Security].[USER_GET_BY_USERNAME]",
            new
            {
                Username = username
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<int> CreateAsync(User user)
    {
        using var connection = CreateConnection();

        return await connection.ExecuteScalarAsync<int>(
            "[Security].[USER_CREATE]",
            new
            {
                user.CompanyId,
                user.FirstName,
                user.LastName,
                user.Username,
                user.Email,
                user.PasswordHash
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<bool> UpdateAsync(User user)
    {
        using var connection = CreateConnection();

        var rowsAffected =
            await connection.ExecuteScalarAsync<int>(
                "[Security].[USER_UPDATE]",
                new
                {
                    user.CompanyId,
                    user.UserId,
                    user.FirstName,
                    user.LastName,
                    user.Username,
                    user.Email,
                    user.PasswordHash,
                    user.IsActive
                },
                commandType: CommandType.StoredProcedure);

        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(
        int companyId,
        int userId)
    {
        using var connection = CreateConnection();

        var rowsAffected =
            await connection.ExecuteScalarAsync<int>(
                "[Security].[USER_DELETE]",
                new
                {
                    CompanyId = companyId,
                    UserId = userId
                },
                commandType: CommandType.StoredProcedure);

        return rowsAffected > 0;
    }
}