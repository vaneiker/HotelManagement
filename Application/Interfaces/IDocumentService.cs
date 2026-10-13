
using HotelManagement.Application.DTOs.Documents;

namespace HotelManagement.Application.Interfaces;

public interface IDocumentService
{
    Task<IEnumerable<DocumentDto>> GetAllAsync(
        int companyId);

    Task<DocumentDto?> GetByIdAsync(
        int companyId,
        long documentId);

    Task<long> UploadAsync(
        int companyId,
        string? entityType,
        long? entityId,
        string originalFileName,
        string contentType,
        long fileSize,
        Stream stream);

    Task<bool> DeleteAsync(
        int companyId,
        long documentId);
}