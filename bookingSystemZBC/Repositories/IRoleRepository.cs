using bookingSystemZBC.Domain.Entities;

namespace bookingSystemZBC.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        public Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}
