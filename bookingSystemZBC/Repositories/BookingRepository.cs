using bookingSystemZBC.Data;
using bookingSystemZBC.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace bookingSystemZBC.Repositories;

public class BookingRepository(AppDbContext dbContext) : Repository<Booking>(dbContext), IBookingRepository
{
    public async Task<IReadOnlyList<Booking>> GetAllDetailedAsync(CancellationToken cancellationToken = default)
        => await DbContext.Bookings
            .AsNoTracking()
            .Include(x => x.Member)
            .Include(x => x.ActivitySession)
                .ThenInclude(x => x!.Activity)
            .ToListAsync(cancellationToken);

    public async Task<Booking?> GetByIdDetailedAsync(int id, CancellationToken cancellationToken = default)
        => await DbContext.Bookings
            .Include(x => x.Member)
            .Include(x => x.ActivitySession)
                .ThenInclude(x => x!.Activity)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<bool> ExistsForMemberAndSessionAsync(
        int memberId,
        int activitySessionId,
        CancellationToken cancellationToken = default)
        => await DbContext.Bookings.AnyAsync(
            x => x.MemberId == memberId && x.ActivitySessionId == activitySessionId,
            cancellationToken);
}
