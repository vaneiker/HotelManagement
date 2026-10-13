
using HotelManagement.Application.DTOs.Documents;
using HotelManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers;

[ApiController]
[Route("api/documents")]
public class DocumentController : ControllerBase
{
    private readonly IDocumentService _documentService;

    public DocumentController(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    [RequestSizeLimit(50 * 1024 * 1024)]
    public async Task<IActionResult> Upload(
     [FromForm] UploadDocumentRequest request)
    {
        var file = request.File;

        if (file == null || file.Length == 0)
            return BadRequest("Debe seleccionar un archivo.");

        if (request.CompanyId <= 0)
            return BadRequest("CompanyId no es válido.");

        if (request.EntityId.HasValue &&
            string.IsNullOrWhiteSpace(request.EntityType))
        {
            return BadRequest(
                "EntityType es obligatorio cuando se especifica EntityId.");
        }

        await using var stream = file.OpenReadStream();

        var documentId = await _documentService.UploadAsync(
            request.CompanyId,
            request.EntityType,
            request.EntityId,
            file.FileName,
            file.ContentType,
            file.Length,
            stream);

        var document = await _documentService.GetByIdAsync(
            request.CompanyId,
            documentId);

        return CreatedAtAction(
            nameof(GetById),
            new
            {
                companyId = request.CompanyId,
                documentId
            },
            document);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int companyId)
    {
        if (companyId <= 0)
        {
            return BadRequest("CompanyId no es válido.");
        }

        var documents = await _documentService.GetAllAsync(companyId);

        return Ok(documents);
    }

    [HttpGet("{documentId:long}")]
    public async Task<IActionResult> GetById(
        [FromQuery] int companyId,
        long documentId)
    {
        if (companyId <= 0)
        {
            return BadRequest("CompanyId no es válido.");
        }

        if (documentId <= 0)
        {
            return BadRequest("DocumentId no es válido.");
        }

        var document = await _documentService.GetByIdAsync(
            companyId,
            documentId);

        if (document is null)
        {
            return NotFound("Documento no encontrado.");
        }

        return Ok(document);
    }

    [HttpDelete("{documentId:long}")]
    public async Task<IActionResult> Delete(
        [FromQuery] int companyId,
        long documentId)
    {
        if (companyId <= 0)
        {
            return BadRequest("CompanyId no es válido.");
        }

        if (documentId <= 0)
        {
            return BadRequest("DocumentId no es válido.");
        }

        var deleted = await _documentService.DeleteAsync(
            companyId,
            documentId);

        if (!deleted)
        {
            return NotFound("Documento no encontrado.");
        }

        return NoContent();
    }
}