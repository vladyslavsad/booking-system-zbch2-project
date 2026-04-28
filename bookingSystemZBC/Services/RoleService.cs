using bookingSystemZBC.Domain.Entities;
using bookingSystemZBC.Repositories;

namespace bookingSystemZBC.Services
{
    public class RoleService(IUserRepository userRepository, IRoleRepository roleRepository) : IRoleService
    {
        public async Task<ICollection<Role>> GetRolesAsync(int userId, CancellationToken cancellationToken = default)
        {
            var user = await userRepository.GetByIdAsync(userId, cancellationToken)
                ?? throw new KeyNotFoundException($"User {userId} was not found");

            return user.Roles;
        }

        public async Task<bool> AssignRole(int userId, string roleName, CancellationToken cancellationToken = default)
        {
            roleName = NormalizeRoleName(roleName);
            var user = await userRepository.GetByIdAsync(userId, cancellationToken)
                ?? throw new KeyNotFoundException($"User {userId} was not found");
            var role = await roleRepository.GetByNameAsync(roleName, cancellationToken)
                ?? throw new KeyNotFoundException($"Role '{roleName}' was not found");

            if (user.Roles.Any(r => r.RoleName.Equals(roleName, StringComparison.OrdinalIgnoreCase)))
            {
                return true;
            }

            user.Roles.Add(role);
            userRepository.Update(user);
            await userRepository.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> RemoveRole(int userId, string roleName, CancellationToken cancellationToken = default)
        {
            roleName = NormalizeRoleName(roleName);
            var user = await userRepository.GetByIdAsync(userId, cancellationToken)
                ?? throw new KeyNotFoundException($"User {userId} was not found");
            var role = await roleRepository.GetByNameAsync(roleName, cancellationToken)
                ?? throw new KeyNotFoundException($"Role '{roleName}' was not found");

            if (user.Roles.Any(r => r.RoleName.Equals(roleName, StringComparison.OrdinalIgnoreCase)))
            {
                user.Roles.Remove(role);
                userRepository.Update(user);
                await userRepository.SaveChangesAsync(cancellationToken);
                return true;
            }

            throw new InvalidOperationException($"Role '{roleName}' was not assigned to user {userId}");
        }

        private static string NormalizeRoleName(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new InvalidOperationException("Role name is required");
            }

            return roleName.Trim();
        }
    }
}
