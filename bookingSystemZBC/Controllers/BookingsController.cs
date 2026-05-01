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
public class BookingsController(
    IBookingService bookingService,
    IAuthorizationService authorizationService,
    IMemberService memberService,
    IServiceScopeFactory scopeFactory) : ControllerBase
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
        ?? throw new BookingExeption();

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
        var booking = await bookingService.GetByIdAsync(id, cancellationToken)
       ?? throw new BookingExeption();

        var result = await authorizationService.AuthorizeAsync(User, booking, ResourceRequirements.CanAccessResources);
        if (!result.Succeeded)
        {
            return Forbid();
        }

        var deleted = await bookingService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }

    [HttpPost("doubleBookingTest")]
    public async Task<IActionResult> DoubleBookingTest(int reqParam, CancellationToken cancellationToken)
    {

        // if reqParam is 0 we call the version of the test that uses the BookingServiceRCTest,
        // which does not have proper concurrency handling, to demonstrate the the race condition while parallel booking 

        if (reqParam == 0)
        {
            BeginRaceConditionTest(1);
        }
        else if (reqParam == 1)
        {
            BeginRaceConditionTest("test");
        }

        return Ok(new
        {
            Message = "Double booking test finished",
        });
    }


    [NonAction]
    public void BeginRaceConditionTest(int param)
    {
        BookingCreateDto booking1 = new BookingCreateDto
        {
            ActivitySessionId = 7,
            MemberId = 1
        };
        BookingCreateDto booking2 = new BookingCreateDto
        {
            ActivitySessionId = 7,
            MemberId = 2
        };
        Thread thread1 = new Thread(() =>
        {
            try
            {
                using var scope = scopeFactory.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<IBookingServiceRCTest>();
                service.CreateAsync(booking1).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Thread1 error: {ex.Message}");
            }
        });
        Thread thread2 = new Thread(() =>
        {
            try
            {
                using var scope = scopeFactory.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<IBookingServiceRCTest>();
                service.CreateAsync(booking2).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Thread2 error: {ex.Message}");
            }
        });
        thread1.Start();
        thread2.Start();
        thread1.Join();
        thread2.Join();
    }


    [NonAction]
    public void BeginRaceConditionTest(string param)
    {
        BookingCreateDto booking1 = new BookingCreateDto
        {
            ActivitySessionId = 7,
            MemberId = 1
        };
        BookingCreateDto booking2 = new BookingCreateDto
        {
            ActivitySessionId = 7,
            MemberId = 2
        };
        Thread thread1 = new Thread(() =>
        {
            try
            {
                using var scope = scopeFactory.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<IBookingService>();
                service.CreateAsync(booking1).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Thread1 error: {ex.Message}");
            }
        });
        Thread thread2 = new Thread(() =>
        {
            try
            {
                using var scope = scopeFactory.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<IBookingService>();
                service.CreateAsync(booking2).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Thread2 error: {ex.Message}");
            }
        });
        thread1.Start();
        thread2.Start();
        thread1.Join();
        thread2.Join();
    }
}


