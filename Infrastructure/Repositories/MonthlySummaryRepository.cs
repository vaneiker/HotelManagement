using Dapper;
using HotelManagement.Application.DTOs.Finance;
using HotelManagement.Application.Interfaces;
using HotelManagement.Domain.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HotelManagement.Infrastructure.Repositories
{
    public sealed class MonthlySummaryRepository : IMonthlySummaryRepository
    {
        private readonly string _connectionString;

        public MonthlySummaryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<IEnumerable<MonthlySummaryDto>> GetMonthlySummaryAsync(int companyId,int propertyId,int year)
        {
            using var connection = CreateConnection();

            var parameters = new
            {
                CompanyId = companyId,
                PropertyId = propertyId,
                Year = year
            };
            var result = await connection.QueryAsync<MonthlySummaryDto>(
                "[Finance].[GET_MONTHLY_SUMMARY]",
                parameters,
                commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}
