using bookingSystemZBC.Data;
using bookingSystemZBC.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace bookingSystemZBC.Repositories
{
    public class RoleRepository(AppDbContext appDbContext) : Repository<Role>(appDbContext), IRoleRepository
    {
        public async Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await DbContext.Roles.FirstOrDefaultAsync(r => r.RoleName == name, cancellationToken);
        }

    }
}
