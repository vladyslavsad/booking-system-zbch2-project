using bookingSystemZBC.Data;
using bookingSystemZBC.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace bookingSystemZBC.Repositories
{
    public class MemberRepository(AppDbContext dbContext) : Repository<Member>(dbContext), IMemberRepository
    {
        public async Task<Member?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var normalizedEmail = NormalizeEmail(email);
            return await DbContext.Members
                .FirstOrDefaultAsync(x => x.Email.ToLower() == normalizedEmail, cancellationToken);
        }

        public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
        {
            var normalizedEmail = NormalizeEmail(email);
            return await DbContext.Members
                .AnyAsync(x => x.Email.ToLower() == normalizedEmail, cancellationToken);
        }

        private static string NormalizeEmail(string email) => email.Trim().ToLowerInvariant();
    }
}
