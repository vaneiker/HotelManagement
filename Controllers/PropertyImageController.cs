using HotelManagement.Application.DTOs.PropertyImages;
using HotelManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers;

[ApiController]
[Route("api/properties/{propertyId:int}/images")]
public class PropertyImageController : ControllerBase
{
    private readonly IPropertyImageService _propertyImageService;

    public PropertyImageController(
        IPropertyImageService propertyImageService)
    {
        _propertyImageService = propertyImageService;
    }

    // GET: api/properties/1/images?companyId=1
    [HttpGet]
    public async Task<IActionResult> GetAll(
        int propertyId,[FromQuery] int companyId)
    {
        if (propertyId <= 0 || companyId <= 0)
            return BadRequest(
                "CompanyId y PropertyId deben ser válidos.");

        var images = await _propertyImageService.GetAllAsync(
            companyId,
            propertyId);

        return Ok(images);
    }

    // GET: api/properties/1/images/10?companyId=1
    [HttpGet("{imageId:long}")]
    public async Task<IActionResult> GetById(
        int propertyId,
        long imageId,
        [FromQuery] int companyId)
    {
        if (propertyId <= 0 || imageId <= 0 || companyId <= 0)
            return BadRequest(
                "Los identificadores deben ser válidos.");

        var image = await _propertyImageService.GetByIdAsync(
            companyId,
            propertyId,
            imageId);

        if (image is null)
            return NotFound("Imagen no encontrada.");

        return Ok(image);
    }

    // POST: api/properties/1/images?companyId=1
    [HttpPost]
    [Consumes("multipart/form-data")]
    [RequestSizeLimit(20 * 1024 * 1024)]
    public async Task<IActionResult> Upload(
        int propertyId,
        [FromQuery] int companyId,
        [FromForm] UploadPropertyImageForm form)
    {
        if (propertyId <= 0 || companyId <= 0)
            return BadRequest(
                "CompanyId y PropertyId deben ser válidos.");

        if (form.File is null || form.File.Length == 0)
            return BadRequest("Debe seleccionar una imagen.");

        if (string.IsNullOrWhiteSpace(form.File.ContentType) ||
            !form.File.ContentType.StartsWith(
                "image/",
                StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest("El archivo debe ser una imagen.");
        }

        await using var stream = form.File.OpenReadStream();

        var request = new UploadPropertyImageRequest
        {
            Stream = stream,
            FileName = Path.GetFileName(form.File.FileName),
            ContentType = form.File.ContentType,
            FileSize = form.File.Length,
            DisplayOrder = form.DisplayOrder,
            IsPrimary = form.IsPrimary
        };

        var imageId = await _propertyImageService.UploadAsync(
            companyId,
            propertyId,
            request);

        var image = await _propertyImageService.GetByIdAsync(
            companyId,
            propertyId,
            imageId);

        return CreatedAtAction(
            nameof(GetById),
            new
            {
                propertyId,
                imageId,
                companyId
            },
            image);
    }

    // PUT: api/properties/1/images/10?companyId=1
    [HttpPut("{imageId:long}")]
    public async Task<IActionResult> Update(
        int propertyId,
        long imageId,
        [FromQuery] int companyId,
        [FromBody] UpdatePropertyImageRequest request)
    {
        if (propertyId <= 0 || imageId <= 0 || companyId <= 0)
            return BadRequest(
                "Los identificadores deben ser válidos.");

        if (request is null)
            return BadRequest("Debe proporcionar los datos.");

        var updated = await _propertyImageService.UpdateAsync(
            companyId,
            propertyId,
            imageId,
            request);

        if (!updated)
            return NotFound("Imagen no encontrada.");

        return NoContent();
    }

    // PUT: api/properties/1/images/10/primary?companyId=1
    [HttpPut("{imageId:long}/primary")]
    public async Task<IActionResult> SetPrimary(
        int propertyId,
        long imageId,
        [FromQuery] int companyId)
    {
        if (propertyId <= 0 || imageId <= 0 || companyId <= 0)
            return BadRequest(
                "Los identificadores deben ser válidos.");

        var updated = await _propertyImageService.SetPrimaryAsync(
            companyId,
            propertyId,
            imageId);

        if (!updated)
            return NotFound(
                "No se pudo establecer la imagen principal.");

        return NoContent();
    }

    // DELETE: api/properties/1/images/10?companyId=1
    [HttpDelete("{imageId:long}")]
    public async Task<IActionResult> Delete(
        int propertyId,
        long imageId,
        [FromQuery] int companyId)
    {
        if (propertyId <= 0 || imageId <= 0 || companyId <= 0)
            return BadRequest(
                "Los identificadores deben ser válidos.");

        var deleted = await _propertyImageService.DeleteAsync(
            companyId,
            propertyId,
            imageId);

        if (!deleted)
            return NotFound("Imagen no encontrada.");

        return NoContent();
    }
}
 
