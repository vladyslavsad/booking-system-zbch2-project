using bookingSystemZBC.DTOs.Users;

namespace bookingSystemZBC.Services
{
    public interface IUserService
    {
        Task<IReadOnlyList<UserDTO>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<UserDTO?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<UserDTO> CreateAsync(UserCreateDTO request, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
