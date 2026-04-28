using bookingSystemZBC.Data;
using bookingSystemZBC.Domain.Entities;
using bookingSystemZBC.Domain.Entities.Activities;
using Microsoft.EntityFrameworkCore;

namespace bookingSystemZBC.Repositories;

public class ActivityRepository(AppDbContext dbContext) : Repository<Activity>(dbContext), IActivityRepository
{
    public async Task<IReadOnlyList<Activity>> GetAllWithSessionsAsync(CancellationToken cancellationToken = default)
        => await DbContext.Activities
            .AsNoTracking()
            .Include(x => x.Sessions)
            .ToListAsync(cancellationToken);
}
