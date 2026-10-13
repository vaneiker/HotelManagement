using HotelManagement.Application.DTOs.UnitTypes;
using HotelManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers;

[ApiController]
[Route("api/properties/{propertyId:int}/unit-types")]
public class UnitTypeController : ControllerBase
{
    private readonly IUnitTypeService _unitTypeService;

    public UnitTypeController(IUnitTypeService unitTypeService)
    {
        _unitTypeService = unitTypeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int propertyId)
    {
        var result = await _unitTypeService
            .GetAllAsync(propertyId);

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        int propertyId,
        int id)
    {
        var result = await _unitTypeService
            .GetByIdAsync(propertyId, id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        int propertyId,
        CreateUnitTypeRequest request)
    {
        var id = await _unitTypeService
            .CreateAsync(propertyId, request);

        return CreatedAtAction(
            nameof(GetById),
            new
            {
                propertyId,
                id
            },
            new
            {
                unitTypeId = id
            });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int propertyId,
        int id,
        UpdateUnitTypeRequest request)
    {
        var updated = await _unitTypeService
            .UpdateAsync(propertyId, id, request);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int propertyId,
        int id)
    {
        var deleted = await _unitTypeService
            .DeleteAsync(propertyId, id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}