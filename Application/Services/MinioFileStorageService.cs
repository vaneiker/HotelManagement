using HotelManagement.Application.Interfaces;
using Minio;
using Minio.DataModel.Args;

namespace HotelManagement.Infrastructure.Services;

public class MinioFileStorageService : IFileStorageService
{
    private readonly IMinioClient _minioClient;

    public MinioFileStorageService(IMinioClient minioClient)
    {
        _minioClient = minioClient;
    }

    public async Task UploadAsync(
        string bucketName,
        string objectKey,
        Stream stream,
        string contentType)
    {
        var bucketExists = await _minioClient.BucketExistsAsync(
            new BucketExistsArgs()
                .WithBucket(bucketName));

        if (!bucketExists)
        {
            await _minioClient.MakeBucketAsync(
                new MakeBucketArgs()
                    .WithBucket(bucketName));
        }

        await _minioClient.PutObjectAsync(
            new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectKey)
                .WithStreamData(stream)
                .WithObjectSize(stream.Length)
                .WithContentType(contentType));
    }

    public async Task<IEnumerable<string>> GetAllAsync(
        string bucketName,
        string prefix)
    {
        var result = new List<string>();

        var args = new ListObjectsArgs()
            .WithBucket(bucketName)
            .WithPrefix(prefix)
            .WithRecursive(true);

        await foreach (var item in _minioClient.ListObjectsEnumAsync(args))
        {
            result.Add(item.Key);
        }

        return result;
    }

    public async Task<Stream> GetAsync(
        string bucketName,
        string objectKey)
    {
        var memoryStream = new MemoryStream();

        await _minioClient.GetObjectAsync(
            new GetObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectKey)
                .WithCallbackStream(stream =>
                {
                    stream.CopyTo(memoryStream);
                }));

        memoryStream.Position = 0;

        return memoryStream;
    }

    public async Task<string> GetUrlAsync(
        string bucketName,
        string objectKey)
    {
        return await _minioClient.PresignedGetObjectAsync(
            new PresignedGetObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectKey)
                .WithExpiry(3600));
    }

    public async Task DeleteAsync(
        string bucketName,
        string objectKey)
    {
        await _minioClient.RemoveObjectAsync(
            new RemoveObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectKey));
    }
}