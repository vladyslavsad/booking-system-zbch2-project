using bookingSystemZBC.Domain.Entities;

namespace bookingSystemZBC.Repositories;

public interface IActivitySessionRepository : IRepository<ActivitySession>
{
    Task<IReadOnlyList<ActivitySession>> GetAllDetailedAsync(CancellationToken cancellationToken = default);
    Task<ActivitySession?> GetByIdDetailedAsync(int id, CancellationToken cancellationToken = default);
    
}
