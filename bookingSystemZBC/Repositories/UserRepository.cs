using bookingSystemZBC.Data;
using bookingSystemZBC.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace bookingSystemZBC.Repositories
{
    public class UserRepository(AppDbContext dbContext) : Repository<User>(dbContext), IUserRepository
    {
        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var normalizedEmail = NormalizeEmail(email);
            return await DbContext.Users
                .Include(u => u.Member)
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(x => x.Email.ToLower() == normalizedEmail, cancellationToken);
        }

        public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await DbContext.Users
                .Include(u => u.Member)
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }
        public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
        {
            var normalizedEmail = NormalizeEmail(email);
            return await DbContext.Users
                .AnyAsync(x => x.Email.ToLower() == normalizedEmail, cancellationToken);
        }

        private static string NormalizeEmail(string email) => email.Trim().ToLowerInvariant();
    }
}
