using HotelManagement.Application.DTOs.Finance;

namespace HotelManagement.Application.Interfaces;

public interface IMonthlySummaryRepository
{
    Task<IEnumerable<MonthlySummaryDto>> GetMonthlySummaryAsync(
        int companyId,
        int propertyId,
        int year);
}
