using HotelManagement.Domain.Entities;

namespace HotelManagement.Application.Interfaces
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetAllAsync(int companyId);

        Task<Property?> GetByIdAsync(int companyId, int propertyId);

        Task<int> CreateAsync(Property property);

        Task<bool> UpdateAsync(Property property);

        Task<bool> DeleteAsync(int companyId, int propertyId);
    }
}
