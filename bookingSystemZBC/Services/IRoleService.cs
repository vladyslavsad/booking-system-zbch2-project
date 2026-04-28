using bookingSystemZBC.Domain.Entities;

namespace bookingSystemZBC.Services
{
    public interface IRoleService
    {
        public Task<ICollection<Role>> GetRolesAsync(int userId, CancellationToken cancellationToken = default);
        public Task<bool> AssignRole(int userId, string roleName, CancellationToken cancellationToken = default);
        public Task<bool> RemoveRole(int userId, string roleName, CancellationToken cancellationToken = default);
    }
}
