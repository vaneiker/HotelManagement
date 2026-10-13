using HotelManagement.Application.DTOs.PropertyImages;

namespace HotelManagement.Application.Interfaces;

public interface IPropertyImageService
{
    Task<IEnumerable<PropertyImageDto>> GetAllAsync(
        int companyId,
        int propertyId);

    Task<PropertyImageDto?> GetByIdAsync(
        int companyId,
        int propertyId,
        long propertyImageId);

    Task<long> UploadAsync(
        int companyId,
        int propertyId,
        UploadPropertyImageRequest request);

    Task<bool> UpdateAsync(
        int companyId,
        int propertyId,
        long propertyImageId,
        UpdatePropertyImageRequest request);

    Task<bool> DeleteAsync(
        int companyId,
        int propertyId,
        long propertyImageId);

    Task<bool> SetPrimaryAsync(
        int companyId,
        int propertyId,
        long propertyImageId);
}