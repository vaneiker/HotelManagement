
using Dapper;
using HotelManagement.Application.DTOs.Documents;
using HotelManagement.Application.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HotelManagement.Infrastructure.Repositories;

public class DocumentRepository : IDocumentRepository
{
    private readonly string _connectionString;

    public DocumentRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<long> CreateAsync(DocumentDto document)
    {
        using var connection = new SqlConnection(_connectionString);

        var parameters = new
        {
            document.CompanyId,
            document.EntityType,
            document.EntityId,
            document.FileName,
            document.OriginalFileName,
            document.BucketName,
            document.ObjectKey,
            document.ContentType,
            document.FileSize
        };

        return await connection.QuerySingleAsync<long>(
            "[Storage].[DOCUMENT_CREATE]",
            parameters,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<DocumentDto>> GetAllAsync(
        int companyId)
    {
        using var connection = new SqlConnection(_connectionString);

        return await connection.QueryAsync<DocumentDto>(
            "[Storage].[DOCUMENT_GET_ALL]",
            new { CompanyId = companyId },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<DocumentDto?> GetByIdAsync(
        int companyId,
        long documentId)
    {
        using var connection = new SqlConnection(_connectionString);

        return await connection.QueryFirstOrDefaultAsync<DocumentDto>(
            "[Storage].[DOCUMENT_GET_BY_ID]",
            new
            {
                CompanyId = companyId,
                DocumentId = documentId
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<bool> DeleteAsync(
        int companyId,
        long documentId)
    {
        using var connection = new SqlConnection(_connectionString);

        var rowsAffected = await connection.QuerySingleAsync<int>(
            "[Storage].[DOCUMENT_DELETE]",
            new
            {
                CompanyId = companyId,
                DocumentId = documentId
            },
            commandType: CommandType.StoredProcedure);

        return rowsAffected > 0;
    }
}