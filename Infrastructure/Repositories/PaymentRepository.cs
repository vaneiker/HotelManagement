using Dapper;
using HotelManagement.Application.DTOs.Payments;
using HotelManagement.Application.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HotelManagement.Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly string _connectionString;

    public PaymentRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<PaymentDto>> GetAllAsync(
        int companyId)
    {
        using var connection = new SqlConnection(_connectionString);

        return await connection.QueryAsync<PaymentDto>(
            "[Billing].[GET_ALL]",
            new
            {
                CompanyId = companyId
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<PaymentDto?> GetByIdAsync(
        int companyId,
        long paymentId)
    {
        using var connection = new SqlConnection(_connectionString);

        return await connection.QueryFirstOrDefaultAsync<PaymentDto>(
            "[Billing].[GET_BY_ID]",
            new
            {
                CompanyId = companyId,
                PaymentId = paymentId
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<PaymentDto>> GetByReservationAsync(
        int companyId,
        long reservationId)
    {
        using var connection = new SqlConnection(_connectionString);

        return await connection.QueryAsync<PaymentDto>(
            "[Billing].[GET_BY_RESERVATION]",
            new
            {
                CompanyId = companyId,
                ReservationId = reservationId
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<long> CreateAsync(
        CreatePaymentRequest request)
    {
        using var connection = new SqlConnection(_connectionString);

        return await connection.QuerySingleAsync<long>(
            "[Billing].[CREATE]",
            new
            {
                request.CompanyId,
                request.ReservationId,
                request.Amount,
                request.CurrencyCode,
                request.PaymentMethod,
                request.PaymentStatus,
                request.Reference,
                request.Notes,
                request.PaymentDate
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<bool> UpdateAsync(
        int companyId,
        long paymentId,
        UpdatePaymentRequest request)
    {
        using var connection = new SqlConnection(_connectionString);

        var rowsAffected = await connection.QuerySingleAsync<int>(
            "[Billing].[UPDATE]",
            new
            {
                CompanyId = companyId,
                PaymentId = paymentId,
                request.Amount,
                request.CurrencyCode,
                request.PaymentMethod,
                request.PaymentStatus,
                request.Reference,
                request.Notes,
                request.PaymentDate
            },
            commandType: CommandType.StoredProcedure);

        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(
        int companyId,
        long paymentId)
    {
        using var connection = new SqlConnection(_connectionString);

        var rowsAffected = await connection.QuerySingleAsync<int>(
            "[Billing].[DELETE]",
            new
            {
                CompanyId = companyId,
                PaymentId = paymentId
            },
            commandType: CommandType.StoredProcedure);

        return rowsAffected > 0;
    }
}