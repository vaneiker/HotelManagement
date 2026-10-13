using HotelManagement.Application.DTOs.Finance;

namespace HotelManagement.Application.Interfaces;

public interface IMonthlySummaryService
{
    Task<IEnumerable<MonthlySummaryDto>> GetMonthlySummaryAsync(
        int companyId,
        int propertyId,
        int year);
}