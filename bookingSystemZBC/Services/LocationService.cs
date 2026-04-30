using bookingSystemZBC.Domain.Entities;
using bookingSystemZBC.DTOs.Locations;
using bookingSystemZBC.Repositories;

namespace bookingSystemZBC.Services;

public class LocationService(ILocationRepository locationRepository) : ILocationService
{
    public async Task<IReadOnlyList<LocationDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var locations = await locationRepository.GetAllAsync(cancellationToken);
        return locations.Select(MapToDto).ToList();
    }

    public async Task<LocationDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var location = await locationRepository.GetByIdAsync(id, cancellationToken);
        return location is null ? null : MapToDto(location);
    }

    public async Task<LocationDto> CreateAsync(LocationCreateDto request, CancellationToken cancellationToken = default)
    {
        var name = request.Name.Trim();
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidOperationException("Location name is required.");
        }

        var location = new Location
        {
            Name = name,
            Description = request.Description,
            Capacity = request.Capacity
        };

        await locationRepository.AddAsync(location, cancellationToken);
        await locationRepository.SaveChangesAsync(cancellationToken);

        return MapToDto(location);

    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var location = await locationRepository.GetByIdAsync(id, cancellationToken);
        if (location is null)
        {
            return false;
        }
        locationRepository.Delete(location);
        await locationRepository.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static LocationDto MapToDto(Location location) => new()
    {
        Id = location.Id,
        Name = location.Name,
        Description = location.Description,
        Capacity = location.Capacity
    };
}
