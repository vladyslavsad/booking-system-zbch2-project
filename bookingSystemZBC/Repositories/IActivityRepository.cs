using bookingSystemZBC.Domain.Entities.Activities;

namespace bookingSystemZBC.Repositories;

public interface IActivityRepository : IRepository<Activity>
{
    Task<IReadOnlyList<Activity>> GetAllWithSessionsAsync(CancellationToken cancellationToken = default);
}
