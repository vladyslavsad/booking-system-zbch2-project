using bookingSystemZBC.Data;
using bookingSystemZBC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace bookingSystemZBC.Repositories;

public class ActivitySessionRepository(AppDbContext dbContext)
    : Repository<ActivitySession>(dbContext), IActivitySessionRepository
{
    public async Task<IReadOnlyList<ActivitySession>> GetAllDetailedAsync(CancellationToken cancellationToken = default)
        => await DbContext.ActivitySessions
            .AsNoTracking()
            .Include(x => x.Activity)
            .Include(x => x.Location)
            .Include(x => x.Bookings)
            .ToListAsync(cancellationToken);

    public async Task<ActivitySession?> GetByIdDetailedAsync(int id, CancellationToken cancellationToken = default)
        => await DbContext.ActivitySessions
            .Include(x => x.Activity)
            .Include(x => x.Location)
            .Include(x => x.Bookings)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);


    public async Task<IReadOnlyList<ActivitySession>> GetAllByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        => await DbContext.ActivitySessions
            .AsNoTracking()
            .Include(x => x.Location)
            .Where(s => s.StartTimeUtc < endDate && s.EndTimeUtc > startDate)
            .ToListAsync(cancellationToken);

}
