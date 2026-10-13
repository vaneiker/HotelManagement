namespace HotelManagement.Application.Interfaces;

public interface IFileStorageService
{
    Task UploadAsync(
        string bucketName,
        string objectKey,
        Stream stream,
        string contentType);

    Task<IEnumerable<string>> GetAllAsync(
        string bucketName,
        string prefix);

    Task<Stream> GetAsync(
        string bucketName,
        string objectKey);

    Task<string> GetUrlAsync(
        string bucketName,
        string objectKey);

    Task DeleteAsync(
        string bucketName,
        string objectKey);
}