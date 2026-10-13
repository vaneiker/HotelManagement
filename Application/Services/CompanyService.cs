using HotelManagement.Application.DTOs.Companies;
using HotelManagement.Application.Interfaces;
using HotelManagement.Domain.Entities;

namespace HotelManagement.Application.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyService(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<IEnumerable<CompanyDto>> GetAllAsync()
    {
        var companies =
            await _companyRepository.GetAllAsync();

        return companies.Select(MapToDto);
    }

    public async Task<CompanyDto?> GetByIdAsync(int companyId)
    {
        var company =
            await _companyRepository.GetByIdAsync(companyId);

        if (company == null)
            return null;

        return MapToDto(company);
    }

    public async Task<int> CreateAsync(
        CreateCompanyRequest request)
    {
        var company = new Company
        {
            Name = request.Name,
            LegalName = request.LegalName,
            Rnc = request.Rnc,
            Email = request.Email,
            Phone = request.Phone,
            Address = request.Address,
            CountryCode = request.CountryCode,
            CurrencyCode = request.CurrencyCode,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        return await _companyRepository.CreateAsync(company);
    }

    public async Task<bool> UpdateAsync(
        int companyId,
        UpdateCompanyRequest request)
    {
        var company =
            await _companyRepository.GetByIdAsync(companyId);

        if (company == null)
            return false;

        company.Name = request.Name;
        company.LegalName = request.LegalName;
        company.Rnc = request.Rnc;
        company.Email = request.Email;
        company.Phone = request.Phone;
        company.Address = request.Address;
        company.CountryCode = request.CountryCode;
        company.CurrencyCode = request.CurrencyCode;
        company.IsActive = request.IsActive;
        company.UpdatedAt = DateTime.UtcNow;

        return await _companyRepository.UpdateAsync(company);
    }

    public async Task<bool> DeleteAsync(int companyId)
    {
        return await _companyRepository.DeleteAsync(companyId);
    }

    private static CompanyDto MapToDto(Company company)
    {
        return new CompanyDto
        {
            CompanyId = company.CompanyId,
            Name = company.Name,
            LegalName = company.LegalName,
            Rnc = company.Rnc,
            Email = company.Email,
            Phone = company.Phone,
            Address = company.Address,
            CountryCode = company.CountryCode,
            CurrencyCode = company.CurrencyCode,
            IsActive = company.IsActive
        };
    }
}