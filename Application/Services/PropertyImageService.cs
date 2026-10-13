using HotelManagement.Application.DTOs.PropertyImages;
using HotelManagement.Application.Interfaces;

namespace HotelManagement.Application.Services;

public class PropertyImageService : IPropertyImageService
{
    private readonly IPropertyImageRepository _repository;
    private readonly IFileStorageService _fileStorage;
    private readonly string _bucketName;

    public PropertyImageService(
        IPropertyImageRepository repository,
        IFileStorageService fileStorage,
        string bucketName)
    {
        _repository = repository;
        _fileStorage = fileStorage;
        _bucketName = bucketName;
    }

    public async Task<IEnumerable<PropertyImageDto>> GetAllAsync(
        int companyId,
        int propertyId)
    {
        var images = await _repository.GetAllAsync(
            companyId, propertyId);

        foreach (var image in images)
        {
            image.Url = await _fileStorage.GetUrlAsync(
                image.BucketName, image.ObjectKey);
        }

        return images;
    }

    public async Task<PropertyImageDto?> GetByIdAsync(
        int companyId,
        int propertyId,
        long propertyImageId)
    {
        var image = await _repository.GetByIdAsync(
            companyId, propertyId, propertyImageId);

        if (image is not null)
        {
            image.Url = await _fileStorage.GetUrlAsync(
                image.BucketName, image.ObjectKey);
        }

        return image;
    }

    public async Task<long> UploadAsync(
        int companyId,
        int propertyId,
        UploadPropertyImageRequest request)
    {
        if (companyId <= 0 || propertyId <= 0)
            throw new ArgumentException(
                "CompanyId y PropertyId deben ser válidos.");

        if (request.Stream is null || !request.Stream.CanRead)
            throw new ArgumentException(
                "El archivo no se puede leer.");

        var extension = Path.GetExtension(request.FileName);

        var objectKey =$"companies/{companyId}/properties/{propertyId}/images/" +
                       $"{Guid.NewGuid():N}{extension}";

        await _fileStorage.UploadAsync(
            _bucketName,
            objectKey,
            request.Stream,
            request.ContentType);

        try
        {
            return await _repository.CreateAsync(
                companyId,
                propertyId,
                request.FileName,
                request.FileName,
                _bucketName,
                objectKey,
                request.ContentType,
                request.FileSize,
                request.DisplayOrder,
                request.IsPrimary);
        }
        catch
        {
            await _fileStorage.DeleteAsync(
                _bucketName, objectKey);

            throw;
        }
    }

    public Task<bool> UpdateAsync(
        int companyId,
        int propertyId,
        long propertyImageId,
        UpdatePropertyImageRequest request)
    {
        return _repository.UpdateAsync(
            companyId,
            propertyId,
            propertyImageId,
            request);
    }

    public async Task<bool> DeleteAsync(
        int companyId,
        int propertyId,
        long propertyImageId)
    {
        var image = await _repository.GetByIdAsync(
            companyId, propertyId, propertyImageId);

        if (image is null)
            return false;

        var deleted = await _repository.DeleteAsync(
            companyId, propertyId, propertyImageId);

        if (!deleted)
            return false;

        await _fileStorage.DeleteAsync(
            image.BucketName, image.ObjectKey);

        return true;
    }

    public Task<bool> SetPrimaryAsync(
        int companyId,
        int propertyId,
        long propertyImageId)
    {
        return _repository.SetPrimaryAsync(
            companyId, propertyId, propertyImageId);
    }
}