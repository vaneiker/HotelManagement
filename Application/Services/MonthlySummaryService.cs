using HotelManagement.Application.DTOs.Finance;
using HotelManagement.Application.Interfaces;

namespace HotelManagement.Application.Services
{
    public sealed class MonthlySummaryService : IMonthlySummaryService
    {
        private readonly IMonthlySummaryRepository _repository;

        public MonthlySummaryService(IMonthlySummaryRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<MonthlySummaryDto>> GetMonthlySummaryAsync(
            int companyId,
            int propertyId,
            int year)
        {
            if (companyId <= 0)
                throw new ArgumentException("La compañía no es válida.");

            if (propertyId <= 0)
                throw new ArgumentException("La propiedad no es válida.");

            if (year < 2000 || year > 2100)
                throw new ArgumentOutOfRangeException(
                    nameof(year), "El año no es válido.");

            return _repository.GetMonthlySummaryAsync(
                companyId, propertyId, year);
        }
    }
}
