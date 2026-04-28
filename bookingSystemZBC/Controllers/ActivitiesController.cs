using bookingSystemZBC.Constants;
using bookingSystemZBC.DTOs.Activities;
using bookingSystemZBC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bookingSystemZBC.Controllers;

[Authorize(Roles = CustomRoles.Admin)]
[ApiController]
[Route("api/[controller]")]
public class ActivitiesController(IActivityService activityService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ActivityDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ActivityDto>>> GetAll(CancellationToken cancellationToken)
        => Ok(await activityService.GetAllAsync(cancellationToken));

    [HttpGet("getById/{id:int}")]
    [ProducesResponseType(typeof(ActivityDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ActivityDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var activity = await activityService.GetByIdAsync(id, cancellationToken);
        return activity is null ? NotFound() : Ok(activity);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ActivityDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ActivityDto>> Create(ActivityCreateDto request, CancellationToken cancellationToken)
    {
        var created = await activityService.CreateAsync(request, cancellationToken);
        return Created($"/api/activities/{created.Id}", created);
    }

    [HttpDelete("delete/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await activityService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
