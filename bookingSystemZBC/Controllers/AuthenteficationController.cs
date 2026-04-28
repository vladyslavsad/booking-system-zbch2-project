using bookingSystemZBC.Repositories;
using bookingSystemZBC.Services;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bookingSystemZBC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenteficationController(IAuthentificationService authService) : ControllerBase
    {
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> Login(string email, string passwird,CancellationToken cancellationToken = default)
        {
            var user = await authService.ValidateUserCredentialsAsync(email, passwird, cancellationToken);
            if (user is null)
            {
                return BadRequest("Invalid email or password.");
            }

            var token = await authService.GenerateTokenAsync(user, cancellationToken);

            if (token is null)
            {
                return BadRequest("Failed to generate token.");
            }
            return Ok(token);
        }

    }
}
