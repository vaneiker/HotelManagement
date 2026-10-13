
using HotelManagement.Application.DTOs.Documents;

namespace HotelManagement.Application.Interfaces;

public interface IDocumentRepository
{
    Task<long> CreateAsync(DocumentDto document);

    Task<IEnumerable<DocumentDto>> GetAllAsync(
        int companyId);

    Task<DocumentDto?> GetByIdAsync(
        int companyId,
        long documentId);

    Task<bool> DeleteAsync(
        int companyId,
        long documentId);
}