using HotelManagement.Application.DTOs.Companies;

namespace HotelManagement.Application.Interfaces;

public interface ICompanyService
{
    Task<IEnumerable<CompanyDto>> GetAllAsync();

    Task<CompanyDto?> GetByIdAsync(int companyId);

    Task<int> CreateAsync(CreateCompanyRequest request);

    Task<bool> UpdateAsync(
        int companyId,
        UpdateCompanyRequest request);

    Task<bool> DeleteAsync(int companyId);
}