using HotelManagement.Application.DTOs.Properties;
using HotelManagement.Application.DTOs.PropertyImages;

namespace HotelManagement.Application.Interfaces
{
    public interface IPropertyService
    {
        Task<IEnumerable<PropertyDto>> GetAllAsync(int companyId);

        Task<PropertyDto?> GetByIdAsync(int companyId, int propertyId);

        Task<int> CreateAsync(int companyId, CreatePropertyRequest request);

        Task<bool> UpdateAsync(
            int companyId,
            int propertyId,
            UpdatePropertyRequest request);

        Task<bool> DeleteAsync(int companyId, int propertyId);

        Task<bool> UpdateAsync(
    int companyId,
    int propertyId,
    long propertyImageId,
    UpdatePropertyImageRequest request);
    }
}
