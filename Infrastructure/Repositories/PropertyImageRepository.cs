using Dapper;
using HotelManagement.Application.DTOs.PropertyImages;
using HotelManagement.Application.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HotelManagement.Infrastructure.Repositories;

public class PropertyImageRepository : IPropertyImageRepository
{
    private readonly string _connectionString;

    public PropertyImageRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    private IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }

    public async Task<IEnumerable<PropertyImageDto>> GetAllAsync(
        int companyId,
        int propertyId)
    {
        using var connection = CreateConnection();

        return await connection.QueryAsync<PropertyImageDto>(
            "[Storage].[PROPERTY_IMAGE_GET_ALL]",
            new { CompanyId = companyId, PropertyId = propertyId },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<PropertyImageDto?> GetByIdAsync(
        int companyId,
        int propertyId,
        long propertyImageId)
    {
        using var connection = CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<PropertyImageDto>(
            "[Storage].[PROPERTY_IMAGE_GET_BY_ID]",
            new
            {
                CompanyId = companyId,
                PropertyId = propertyId,
                PropertyImageId = propertyImageId
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<long> CreateAsync(
        int companyId,
        int propertyId,
        string fileName,
        string originalFileName,
        string bucketName,
        string objectKey,
        string contentType,
        long fileSize,
        int displayOrder,
        bool isPrimary)
    {
        using var connection = CreateConnection();

        return await connection.QuerySingleAsync<long>(
            "[Storage].[PROPERTY_IMAGE_CREATE]",
            new
            {
                CompanyId = companyId,
                PropertyId = propertyId,
                FileName = fileName,
                OriginalFileName = originalFileName,
                BucketName = bucketName,
                ObjectKey = objectKey,
                ContentType = contentType,
                FileSize = fileSize,
                DisplayOrder = displayOrder,
                IsPrimary = isPrimary
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<bool> UpdateAsync(
        int companyId,
        int propertyId,
        long propertyImageId,
        UpdatePropertyImageRequest request)
    {
        using var connection = CreateConnection();

        var result = await connection.QuerySingleAsync<int>(
            "[Storage].[PROPERTY_IMAGE_UPDATE]",
            new
            {
                CompanyId = companyId,
                PropertyId = propertyId,
                PropertyImageId = propertyImageId,
                request.DisplayOrder,
                request.IsPrimary,
                request.IsActive
            },
            commandType: CommandType.StoredProcedure);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(
        int companyId,
        int propertyId,
        long propertyImageId)
    {
        using var connection = CreateConnection();

        var result = await connection.QuerySingleAsync<int>(
            "[Storage].[PROPERTY_IMAGE_DELETE]",
            new
            {
                CompanyId = companyId,
                PropertyId = propertyId,
                PropertyImageId = propertyImageId
            },
            commandType: CommandType.StoredProcedure);

        return result > 0;
    }

    public async Task<bool> SetPrimaryAsync(
        int companyId,
        int propertyId,
        long propertyImageId)
    {
        using var connection = CreateConnection();

        var result = await connection.QuerySingleAsync<int>(
            "[Storage].[PROPERTY_IMAGE_SET_PRIMARY]",
            new
            {
                CompanyId = companyId,
                PropertyId = propertyId,
                PropertyImageId = propertyImageId
            },
            commandType: CommandType.StoredProcedure);

        return result > 0;
    }
}