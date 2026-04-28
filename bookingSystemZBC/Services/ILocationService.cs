using bookingSystemZBC.DTOs.Locations;

namespace bookingSystemZBC.Services;

public interface ILocationService
{
    Task<IReadOnlyList<LocationDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<LocationDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<LocationDto> CreateAsync(LocationCreateDto request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
