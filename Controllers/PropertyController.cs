using HotelManagement.Application.DTOs.Properties;
using HotelManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PropertyController : ControllerBase
{
    private readonly IPropertyService _propertyService;

    public PropertyController(IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var companyId = 1;

        var properties = await _propertyService
            .GetAllAsync(companyId);

        return Ok(properties);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var companyId = 1;

        var property = await _propertyService
            .GetByIdAsync(companyId, id);

        if (property == null)
            return NotFound();

        return Ok(property);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreatePropertyRequest request)
    {
        var companyId = 1;

        var propertyId = await _propertyService
            .CreateAsync(companyId, request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = propertyId },
            new { propertyId });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdatePropertyRequest request)
    {
        var companyId = 1;

        var updated = await _propertyService
            .UpdateAsync(companyId, id, request);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var companyId = 1;

        var deleted = await _propertyService
            .DeleteAsync(companyId, id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}