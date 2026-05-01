using bookingSystemZBC.Constants;
using bookingSystemZBC.Data;
using bookingSystemZBC.Domain.Entities;
using bookingSystemZBC.DTOs.Users;
using bookingSystemZBC.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace bookingSystemZBC.Services
{
    public class AuthenticationService(IConfiguration cfg, AppDbContext appDbContext, IUserRepository userRepository) : IAuthenticationService
    {
        public async Task<UserDTO> ValidateUserCredentialsAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            var user = await userRepository.GetByEmailAsync(email, cancellationToken);
            if (user == null)
                return null;
            // In a real application, you should hash the password and compare it with the stored hash
            if (user.Password != password) return null;

            return MapToDto(user);
        }

        public async Task<string> GenerateTokenAsync(UserDTO user, CancellationToken cancellationToken)
        {
            var jwtSection = cfg.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(CustomClaims.MemberId, user.MemberId.ToString() ?? "")
            };

            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
            }

            var token = new JwtSecurityToken(
                issuer: jwtSection["Issuer"],
                audience: jwtSection["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    int.TryParse(jwtSection["AccessTokenMinutes"], out var minutes) ? minutes : 30),
                signingCredentials: creds
            );

            var jwtTokenHandler = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtTokenHandler;
        }
        private static UserDTO MapToDto(User user) => new()
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            FirstName = user.FirstName,
            Surname = user.Surname,
            MemberId = user.MemberId,
            Roles = user.Roles.ToList()
        };
    }


}
