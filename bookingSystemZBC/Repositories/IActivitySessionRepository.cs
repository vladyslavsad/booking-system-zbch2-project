using bookingSystemZBC.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace bookingSystemZBC.Repositories;

public interface IActivitySessionRepository : IRepository<ActivitySession>
{
    Task<IReadOnlyList<ActivitySession>> GetAllDetailedAsync(CancellationToken cancellationToken = default);
    Task<ActivitySession?> GetByIdDetailedAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ActivitySession>> GetAllByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    
}
