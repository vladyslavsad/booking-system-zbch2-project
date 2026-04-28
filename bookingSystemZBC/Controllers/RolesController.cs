using bookingSystemZBC.Constants;
using bookingSystemZBC.DTOs.Roles;
using bookingSystemZBC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bookingSystemZBC.Controllers;

[Authorize(Roles = CustomRoles.Admin)]
[ApiController]
[Route("api/[controller]")]
public class RolesController(IRoleService roleService) : ControllerBase
{
    [HttpGet("users/{userId:int}/roles")]
    [ProducesResponseType(typeof(IReadOnlyList<RoleDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<RoleDto>>> GetRoles(int userId, CancellationToken cancellationToken = default)
    {
        var roles = await roleService.GetRolesAsync(userId, cancellationToken);

        return Ok(roles.Select(role => new RoleDto
        {
            RoleId = role.RoleId,
            RoleName = role.RoleName
        }).ToList());
    }

    [HttpPost("users/{userId:int}/roles/{roleName}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AssignRole(int userId, string roleName, CancellationToken cancellationToken = default)
    {
        await roleService.AssignRole(userId, roleName, cancellationToken);
        return NoContent();
    }

    [HttpDelete("users/{userId:int}/roles/{roleName}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveRole(int userId, string roleName, CancellationToken cancellationToken = default)
    {
        await roleService.RemoveRole(userId, roleName, cancellationToken);
        return NoContent();
    }
}
