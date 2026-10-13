using HotelManagement.Application.DTOs.Payments;
using HotelManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _service;

    public PaymentController(IPaymentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int companyId)
    {
        var result = await _service.GetAllAsync(companyId);

        return Ok(result);
    }

    [HttpGet("{paymentId:long}")]
    public async Task<IActionResult> GetById(
        long paymentId,
        [FromQuery] int companyId)
    {
        var result = await _service.GetByIdAsync(
            companyId,
            paymentId);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("reservation/{reservationId:long}")]
    public async Task<IActionResult> GetByReservation(
        long reservationId,
        [FromQuery] int companyId)
    {
        var result = await _service.GetByReservationAsync(
            companyId,
            reservationId);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreatePaymentRequest request)
    {
        var paymentId = await _service.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new
            {
                paymentId,
                companyId = request.CompanyId
            },
            new
            {
                PaymentId = paymentId
            });
    }

    [HttpPut("{paymentId:long}")]
    public async Task<IActionResult> Update(
        long paymentId,
        [FromQuery] int companyId,
        UpdatePaymentRequest request)
    {
        var updated = await _service.UpdateAsync(
            companyId,
            paymentId,
            request);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{paymentId:long}")]
    public async Task<IActionResult> Delete(
        long paymentId,
        [FromQuery] int companyId)
    {
        var deleted = await _service.DeleteAsync(
            companyId,
            paymentId);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}