using bookingSystemZBC.Constants;
using bookingSystemZBC.Domain.Entities;
using bookingSystemZBC.DTOs.Sessions;
using bookingSystemZBC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bookingSystemZBC.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SessionsController(IActivitySchedulingService activitySchedulingService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ActivitySessionDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ActivitySessionDto>>> GetAll(CancellationToken cancellationToken)
        => Ok(await activitySchedulingService.GetAllAsync(cancellationToken));

    
    [HttpGet("getById/{id:int}")]
    [ProducesResponseType(typeof(ActivitySessionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ActivitySessionDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var session = await activitySchedulingService.GetByIdAsync(id, cancellationToken);
        return session is null ? NotFound() : Ok(session);
    }

    [Authorize(Roles = CustomRoles.Admin)]
    [HttpPost]
    [ProducesResponseType(typeof(ActivitySessionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ActivitySessionDto>> Create(ActivitySessionCreateDto request, CancellationToken cancellationToken)
    {
        var created = await activitySchedulingService.CreateAsync(request, cancellationToken);
        return Ok(created);
    }

    [Authorize(Roles = CustomRoles.Admin)]
    [HttpDelete("delete/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var session = await activitySchedulingService.GetByIdAsync(id, cancellationToken);
        
        if (session is null)
        {
            return NotFound();
        }
        var deleted = await activitySchedulingService.DeleteActivitySession(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
