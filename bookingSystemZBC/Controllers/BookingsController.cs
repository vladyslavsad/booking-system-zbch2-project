using Azure.Core;
using bookingSystemZBC.Constants;
using bookingSystemZBC.Domain.Entities;
using bookingSystemZBC.DTOs.Bookings;
using bookingSystemZBC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace bookingSystemZBC.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController(IBookingService bookingService, IAuthorizationService authorizationService, IMemberService memberService) : ControllerBase
{
    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<BookingDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<BookingDto>>> GetAll(CancellationToken cancellationToken)
        => Ok(await bookingService.GetAllAsync(cancellationToken));

    [HttpGet("getById/{id:int}")]
    [ProducesResponseType(typeof(BookingDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BookingDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var booking = await bookingService.GetByIdAsync(id, cancellationToken);
        if (booking == null)
        {
            return NotFound();
        }
        var result = await authorizationService.AuthorizeAsync(User, booking, ResourceRequirements.CanAccessResources);
        if (!result.Succeeded) 
        { 
            return Forbid();
        }
        return Ok(booking);
    }

    [HttpPost]
    [ProducesResponseType(typeof(BookingDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BookingDto>> Create(BookingCreateDto request, CancellationToken cancellationToken)
    {
        var member = await memberService.GetByIdAsync(request.MemberId, cancellationToken)
        ?? throw new KeyNotFoundException($"Member {request.MemberId} not found");

        var result = await authorizationService.AuthorizeAsync(User, member, ResourceRequirements.CanAccessResources);
        if (!result.Succeeded)
        {
            return Forbid();
        }
        var created = await bookingService.CreateAsync(request, cancellationToken);
        return Created($"/api/bookings/{created.Id}", created);
    }

    [HttpDelete("delete/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var member = await memberService.GetByIdAsync(id, cancellationToken)
       ?? throw new KeyNotFoundException($"Member {id} not found");

        var result = await authorizationService.AuthorizeAsync(User, member, ResourceRequirements.CanAccessResources);
        if (!result.Succeeded)
        {
            return Forbid();
        }

        var deleted = await bookingService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
