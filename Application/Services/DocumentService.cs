
using HotelManagement.Application.DTOs.Documents;
using HotelManagement.Application.Interfaces;

namespace HotelManagement.Application.Services;

public class DocumentService : IDocumentService
{
    private readonly IDocumentRepository _documentRepository;
    private readonly IFileStorageService _fileStorageService;
    private readonly string _bucketName;

    public DocumentService(
        IDocumentRepository documentRepository,
        IFileStorageService fileStorageService,
        string bucketName)
    {
        _documentRepository = documentRepository;
        _fileStorageService = fileStorageService;
        _bucketName = bucketName;
    }

    public async Task<IEnumerable<DocumentDto>> GetAllAsync(
        int companyId)
    {
        var documents = await _documentRepository.GetAllAsync(companyId);

        foreach (var document in documents)
        {
            document.Url = await _fileStorageService.GetUrlAsync(
                document.BucketName,
                document.ObjectKey);
        }

        return documents;
    }

    public async Task<DocumentDto?> GetByIdAsync(
        int companyId,
        long documentId)
    {
        var document = await _documentRepository.GetByIdAsync(
            companyId,
            documentId);

        if (document is null)
        {
            return null;
        }

        document.Url = await _fileStorageService.GetUrlAsync(
            document.BucketName,
            document.ObjectKey);

        return document;
    }

    public async Task<long> UploadAsync(
        int companyId,
        string? entityType,
        long? entityId,
        string originalFileName,
        string contentType,
        long fileSize,
        Stream stream)
    {
        if (companyId <= 0)
        {
            throw new ArgumentException("CompanyId no es válido.");
        }

        if (entityId.HasValue && string.IsNullOrWhiteSpace(entityType))
        {
            throw new ArgumentException(
                "EntityType es obligatorio cuando se especifica EntityId.");
        }

        var extension = Path.GetExtension(originalFileName);
        var fileName = $"{Guid.NewGuid():N}{extension}";
        var objectKey = $"documents/{companyId}/{fileName}";

        await _fileStorageService.UploadAsync(
            _bucketName,
            objectKey,
            stream,
            contentType);

        try
        {
            var document = new DocumentDto
            {
                CompanyId = companyId,
                EntityType = entityType,
                EntityId = entityId,
                FileName = fileName,
                OriginalFileName = Path.GetFileName(originalFileName),
                BucketName = _bucketName,
                ObjectKey = objectKey,
                ContentType = contentType,
                FileSize = fileSize,
                IsActive = true
            };

            return await _documentRepository.CreateAsync(document);
        }
        catch
        {
            // Evita dejar el archivo en MinIO si falla el registro SQL.
            await _fileStorageService.DeleteAsync(_bucketName, objectKey);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(
        int companyId,
        long documentId)
    {
        var document = await _documentRepository.GetByIdAsync(
            companyId,
            documentId);

        if (document is null)
        {
            return false;
        }

        // Primero eliminamos el objeto físico.
        await _fileStorageService.DeleteAsync(
            document.BucketName,
            document.ObjectKey);

        // Después desactivamos el registro SQL.
        return await _documentRepository.DeleteAsync(
            companyId,
            documentId);
    }
}