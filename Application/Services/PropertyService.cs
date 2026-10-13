using HotelManagement.Application.DTOs.Properties;
using HotelManagement.Application.DTOs.PropertyImages;
using HotelManagement.Application.Interfaces;
using HotelManagement.Domain.Entities;

namespace HotelManagement.Application.Services;

public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _propertyRepository;

    public PropertyService(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    public async Task<IEnumerable<PropertyDto>> GetAllAsync(int companyId)
    {
        var properties = await _propertyRepository.GetAllAsync(companyId);

        return properties.Select(MapToDto);
    }

    public async Task<PropertyDto?> GetByIdAsync(
        int companyId,
        int propertyId)
    {
        var property = await _propertyRepository
            .GetByIdAsync(companyId, propertyId);

        if (property == null)
            return null;

        return MapToDto(property);
    }

    public async Task<int> CreateAsync(
        int companyId,
        CreatePropertyRequest request)
    {
        var property = new Property
        {
            CompanyId = companyId,
            Name = request.Name,
            PropertyType = request.PropertyType,
            Description = request.Description,
            Address = request.Address,
            City = request.City,
            Province = request.Province,
            CountryCode = request.CountryCode,
            Phone = request.Phone,
            Email = request.Email,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        return await _propertyRepository.CreateAsync(property);
    }

    public async Task<bool> UpdateAsync(
        int companyId,
        int propertyId,
        UpdatePropertyRequest request)
    {
        var property = await _propertyRepository
            .GetByIdAsync(companyId, propertyId);

        if (property == null)
            return false;

        property.Name = request.Name;
        property.PropertyType = request.PropertyType;
        property.Description = request.Description;
        property.Address = request.Address;
        property.City = request.City;
        property.Province = request.Province;
        property.CountryCode = request.CountryCode;
        property.Phone = request.Phone;
        property.Email = request.Email;
        property.IsActive = request.IsActive;
        property.UpdatedAt = DateTime.UtcNow;

        return await _propertyRepository.UpdateAsync(property);
    }

    public async Task<bool> DeleteAsync(
        int companyId,
        int propertyId)
    {
        return await _propertyRepository
            .DeleteAsync(companyId, propertyId);
    }

    private static PropertyDto MapToDto(Property property)
    {
        return new PropertyDto
        {
            PropertyId = property.PropertyId,
            CompanyId = property.CompanyId,
            Name = property.Name,
            PropertyType = property.PropertyType,
            Description = property.Description,
            Address = property.Address,
            City = property.City,
            Province = property.Province,
            CountryCode = property.CountryCode,
            Phone = property.Phone,
            Email = property.Email,
            IsActive = property.IsActive
        };
    }

    public Task<bool> UpdateAsync(int companyId, int propertyId, long propertyImageId, UpdatePropertyImageRequest request)
    {
        throw new NotImplementedException();
    }
}