using HotelManagement.Application.DTOs.PropertyImages;

namespace HotelManagement.Application.Interfaces;

public interface IPropertyImageRepository
{
    Task<IEnumerable<PropertyImageDto>> GetAllAsync(
        int companyId,
        int propertyId);

    Task<PropertyImageDto?> GetByIdAsync(
        int companyId,
        int propertyId,
        long propertyImageId);

    Task<long> CreateAsync(
        int companyId,
        int propertyId,
        string fileName,
        string originalFileName,
        string bucketName,
        string objectKey,
        string contentType,
        long fileSize,
        int displayOrder,
        bool isPrimary);

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