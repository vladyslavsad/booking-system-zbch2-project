using bookingSystemZBC.Constants;
using bookingSystemZBC.DTOs.Members;
using bookingSystemZBC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bookingSystemZBC.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController(IMemberService memberService, IAuthorizationService authorizationService) : ControllerBase
    {
        [Authorize(Roles = CustomRoles.Admin)]
        [HttpGet("getAll")]
        [ProducesResponseType(typeof(IReadOnlyCollection<MemberDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyCollection<MemberDTO>>> GetAll(CancellationToken cancellationToken = default)
        {
            var members = await memberService.GetAllAsync(cancellationToken);
            if (members is null)
                return NotFound();
            return Ok(members);
        }

        [HttpGet("getById/{id:int}")]
        [ProducesResponseType(typeof(MemberDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MemberDTO>> GetById(int id, CancellationToken cancellationToken = default)
        {
            var member = await memberService.GetByIdAsync(id, cancellationToken);
            if (member is null)
            {
                return NotFound();
            }
            var result = await authorizationService.AuthorizeAsync(User, member, ResourceRequirements.CanAccessResources);
            if (!result.Succeeded)
            {
                return Forbid();
            }
            return Ok(member);
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(MemberDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MemberDTO>> Create(MemberCreateDTO request, CancellationToken cancellationToken = default)
        {
            var createdMember = await memberService.CreateAsync(request, cancellationToken);
            return Ok(createdMember);
        }

        [HttpDelete("delete/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            var member = await memberService.GetByIdAsync(id, cancellationToken);
            var result = await authorizationService.AuthorizeAsync(User, member, ResourceRequirements.CanAccessResources);
            if (!result.Succeeded)
            {
                return Forbid();
            }
            var deletedMember = await memberService.Delete(id, cancellationToken);
            if(deletedMember == true) return Ok();
            else return NotFound();
        }

    }
}