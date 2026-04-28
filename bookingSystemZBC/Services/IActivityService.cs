using bookingSystemZBC.DTOs.Activities;

namespace bookingSystemZBC.Services;

public interface IActivityService
{
    Task<IReadOnlyList<ActivityDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ActivityDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<ActivityDto> CreateAsync(ActivityCreateDto request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
