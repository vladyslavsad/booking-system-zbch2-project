using bookingSystemZBC.Domain.Entities;

namespace bookingSystemZBC.Repositories;

public interface IBookingRepository : IRepository<Booking>
{
    Task<IReadOnlyList<Booking>> GetAllDetailedAsync(CancellationToken cancellationToken = default);
    Task<Booking?> GetByIdDetailedAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsForMemberAndSessionAsync(int memberId, int activitySessionId, CancellationToken cancellationToken = default);
}
