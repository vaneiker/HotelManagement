using HotelManagement.Domain.Entities;

namespace HotelManagement.Application.Interfaces;

public interface ICompanyRepository
{
    Task<IEnumerable<Company>> GetAllAsync();

    Task<Company?> GetByIdAsync(int companyId);

    Task<int> CreateAsync(Company company);

    Task<bool> UpdateAsync(Company company);

    Task<bool> DeleteAsync(int companyId);
}