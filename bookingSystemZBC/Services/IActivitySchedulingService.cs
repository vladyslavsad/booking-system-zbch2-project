using bookingSystemZBC.DTOs.Sessions;

namespace bookingSystemZBC.Services;

public interface IActivitySchedulingService
{
    Task<IReadOnlyList<ActivitySessionDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ActivitySessionDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> DeleteActivitySession(int id, CancellationToken cancellationToken = default);
    Task<ActivitySessionDto> CreateAsync(ActivitySessionCreateDto request, CancellationToken cancellationToken = default);
}
