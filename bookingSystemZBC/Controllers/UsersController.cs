using bookingSystemZBC.DTOs.Members;
using bookingSystemZBC.DTOs.Users;
using bookingSystemZBC.Services;
using Microsoft.AspNetCore.Mvc;

namespace bookingSystemZBC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService userService, IMemberService memberService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<UserDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<UserDTO>>> GetAll(CancellationToken cancellationToken = default)
            => Ok(await userService.GetAllAsync(cancellationToken));

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDTO>> GetById(int id, CancellationToken cancellationToken = default)
        {
            var user = await userService.GetByIdAsync(id, cancellationToken);
            return user is null ? NotFound() : Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDTO>> Create(UserCreateDTO request, CancellationToken cancellationToken = default)
        {
            var createdUser = await userService.CreateAsync(request, cancellationToken);

            var member = new MemberCreateDTO {
             Name = createdUser.FirstName + " " + createdUser.Surname,
             Email = createdUser.Email,
             Age = request.Age
            };

            var createdMember = await memberService.CreateAsync(member, cancellationToken);
            createdUser.Member = createdMember;

            return Ok(createdUser);

        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            var deleted = await userService.DeleteAsync(id, cancellationToken);
            var member = await memberService.GetByIdAsync(id, cancellationToken);
            if (member is not null)
            {
                var deletedMember = await memberService.Delete(member.Id, cancellationToken);
            }
            return deleted ? NoContent() : NotFound();
        }
    }
}
