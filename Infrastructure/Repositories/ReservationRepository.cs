using Dapper;
using HotelManagement.Application.Interfaces;
using HotelManagement.Domain.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HotelManagement.Infrastructure.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly string _connectionString;

    public ReservationRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    private IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }

    public async Task<IEnumerable<Reservation>> GetAllAsync(
        int companyId)
    {
        using var connection = CreateConnection();

        return await connection.QueryAsync<Reservation>(
            "[Reservation].[GET_ALL]",
            new
            {
                CompanyId = companyId
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<Reservation?> GetByIdAsync(
        int companyId,
        long reservationId)
    {
        using var connection = CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<Reservation>(
            "[Reservation].[GET_BY_ID]",
            new
            {
                CompanyId = companyId,
                ReservationId = reservationId
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<long> CreateAsync(
        Reservation reservation)
    {
        using var connection = CreateConnection();

        return await connection.ExecuteScalarAsync<long>(
            "[Reservation].[CREATE]",
            new
            {
                reservation.CompanyId,
                reservation.PropertyId,
                reservation.UnitId,
                reservation.GuestId,
                reservation.ReservationNumber,
                reservation.CheckInDate,
                reservation.CheckOutDate,
                reservation.Adults,
                reservation.Children,
                reservation.Status,
                reservation.Notes
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<bool> UpdateAsync(
        Reservation reservation)
    {
        using var connection = CreateConnection();

        var rowsAffected =
            await connection.ExecuteScalarAsync<int>(
                "[Reservation].[UPDATE]",
                new
                {
                    reservation.CompanyId,
                    reservation.ReservationId,
                    reservation.PropertyId,
                    reservation.UnitId,
                    reservation.GuestId,
                    reservation.ReservationNumber,
                    reservation.CheckInDate,
                    reservation.CheckOutDate,
                    reservation.Adults,
                    reservation.Children,
                    reservation.Status,
                    reservation.Notes
                },
                commandType: CommandType.StoredProcedure);

        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(
        int companyId,
        long reservationId)
    {
        using var connection = CreateConnection();

        var rowsAffected =
            await connection.ExecuteScalarAsync<int>(
                "[Reservation].[DELETE]",
                new
                {
                    CompanyId = companyId,
                    ReservationId = reservationId
                },
                commandType: CommandType.StoredProcedure);

        return rowsAffected > 0;
    }
}