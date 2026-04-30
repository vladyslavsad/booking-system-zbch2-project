using bookingSystemZBC.Domain.Entities;
using bookingSystemZBC.DTOs.Sessions;
using bookingSystemZBC.Repositories;

namespace bookingSystemZBC.Services;

public class ActivitySchedulingService(
    IActivitySessionRepository activitySessionRepository,
    IActivityRepository activityRepository,
    IRepository<Location> locationRepository) : IActivitySchedulingService
{
    public async Task<IReadOnlyList<ActivitySessionDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var sessions = await activitySessionRepository.GetAllDetailedAsync(cancellationToken);
        return sessions.Select(MapToDto).ToList();
    }

    public async Task<ActivitySessionDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var session = await activitySessionRepository.GetByIdDetailedAsync(id, cancellationToken);
        return session is null ? null : MapToDto(session);
    }

    public async Task<ActivitySessionDto> CreateAsync(ActivitySessionCreateDto request, CancellationToken cancellationToken = default)
    {
        var activity = await activityRepository.GetByIdAsync(request.ActivityId, cancellationToken)
            ?? throw new KeyNotFoundException($"Activity {request.ActivityId} was not found.");

        var location = await locationRepository.GetByIdAsync(request.LocationId, cancellationToken)
            ?? throw new KeyNotFoundException($"Location {request.LocationId} was not found.");

        var isLocationAvailable = await this.isLocationAvailable(request.LocationId, request.StartTimeUtc, request.EndTimeUtc, cancellationToken);
        //fix is available logic
        if (!isLocationAvailable)
        {
            throw new InvalidOperationException("Location is not available.");
        }

        var session = new ActivitySession
        {
            ActivityId = request.ActivityId,
            LocationId = request.LocationId,
            StartTimeUtc = request.StartTimeUtc,
            EndTimeUtc = request.EndTimeUtc,
            Notes = request.Notes
        };

        await activitySessionRepository.AddAsync(session, cancellationToken);
        await activitySessionRepository.SaveChangesAsync(cancellationToken);

        locationRepository.Update(location);
        await locationRepository.SaveChangesAsync(cancellationToken);

        session.Activity = activity;
        session.Location = location;

        return MapToDto(session);
    }

    public async Task<bool> DeleteActivitySession(int id, CancellationToken cancellationToken = default)
    {
        var session = await activitySessionRepository.GetByIdAsync(id, cancellationToken);
        if(session is null)
        {
            return false;
        }
        activitySessionRepository.Delete(session);
        await activitySessionRepository.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> isLocationAvailable(int locationId, DateTime startTimeUtc, DateTime endTimeUtc, CancellationToken cancellationToken = default)
    {
        var sessionsAtLocation = await activitySessionRepository.GetAllByDateRangeAsync(startTimeUtc, endTimeUtc, cancellationToken);
        return !sessionsAtLocation.Any(s => s.LocationId == locationId);
    }

    private static ActivitySessionDto MapToDto(ActivitySession session) => new()
    {
        Id = session.Id,
        ActivityId = session.ActivityId,
        ActivityName = session.Activity?.Name ?? string.Empty,
        LocationId = session.LocationId,
        LocationName = session.Location?.Name ?? string.Empty,
        StartTimeUtc = session.StartTimeUtc,
        EndTimeUtc = session.EndTimeUtc,
        Notes = session.Notes,
        BookingCount = session.Bookings.Count,
        Capacity = session.Activity?.MaxParticipants ?? 0
    };
}
