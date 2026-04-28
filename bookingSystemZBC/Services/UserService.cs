using bookingSystemZBC.Domain.Entities;
using bookingSystemZBC.DTOs.Users;
using bookingSystemZBC.Repositories;

namespace bookingSystemZBC.Services
{
    public class UserService(
        IUserRepository userRepository,
        IMemberRepository memberRepository,
        IRoleRepository roleRepository) : IUserService
    {
        public async Task<IReadOnlyList<UserDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var users = await userRepository.GetAllAsync(cancellationToken);
            return users.Select(MapToDto).ToList();
        }

        public async Task<UserDTO?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var user = await userRepository.GetByIdAsync(id, cancellationToken);
            return user is null ? null : MapToDto(user);
        }

        public async Task<UserDTO> CreateAsync(UserCreateDTO request, CancellationToken cancellationToken = default)
        {
            var email = request.Email.Trim();
            var userName = request.UserName.Trim();
            var firstName = request.FirstName.Trim();
            var surname = request.Surname.Trim();

            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new InvalidOperationException("User name is required.");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new InvalidOperationException("User email is required.");
            }

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                throw new InvalidOperationException("Password is required.");
            }

            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new InvalidOperationException("First name is required.");
            }

            if (string.IsNullOrWhiteSpace(surname))
            {
                throw new InvalidOperationException("Surname is required.");
            }

            await EnsureEmailAvailableAsync(email, cancellationToken);

            if (request.MemberId.HasValue)
            {
                _ = await memberRepository.GetByIdAsync(request.MemberId.Value, cancellationToken)
                    ?? throw new KeyNotFoundException($"Member {request.MemberId.Value} was not found.");
            }

            var user = new User
            {
                UserName = userName,
                Email = email,
                Password = request.Password,
                FirstName = firstName,
                Surname = surname,
            };

            var defoultRole = await roleRepository.GetByNameAsync("User") ?? throw new InvalidOperationException("No User role was found");
            user.Roles.Add(defoultRole);

            await userRepository.AddAsync(user, cancellationToken);
            await userRepository.SaveChangesAsync(cancellationToken);

            return MapToDto(user);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var user = await userRepository.GetByIdAsync(id, cancellationToken);
            if (user is null)
            {
                return false;
            }

            userRepository.Delete(user);
            await userRepository.SaveChangesAsync(cancellationToken);
            return true;
        }

        private async Task EnsureEmailAvailableAsync(string email, CancellationToken cancellationToken)
        {
            var emailExists = await userRepository.EmailExistsAsync(email, cancellationToken);

            if (emailExists)
            {
                throw new InvalidOperationException($"An account with email '{email}' already exists.");
            }
        }

        private static UserDTO MapToDto(User user) => new()
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            FirstName = user.FirstName,
            Surname = user.Surname,
            MemberId = user.MemberId
        };
    }
}
