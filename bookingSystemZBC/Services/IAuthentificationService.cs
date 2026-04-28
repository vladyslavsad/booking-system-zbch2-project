using bookingSystemZBC.Domain.Entities;
using bookingSystemZBC.DTOs.Users;
using bookingSystemZBC.Repositories;

namespace bookingSystemZBC.Services
{
    public interface IAuthentificationService
    {
        Task<UserDTO> ValidateUserCredentialsAsync(string email, string password, CancellationToken cancellationToken = default);
        Task<string> GenerateTokenAsync(UserDTO user, CancellationToken cancellationToken = default);
    }
}
