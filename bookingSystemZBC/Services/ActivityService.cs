using bookingSystemZBC.Domain.Entities.Activities;
using bookingSystemZBC.DTOs.Activities;
using bookingSystemZBC.Factories;
using bookingSystemZBC.Repositories;

namespace bookingSystemZBC.Services;

public class ActivityService(IActivityRepository activityRepository) : IActivityService
{
    public async Task<IReadOnlyList<ActivityDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var activities = await activityRepository.GetAllAsync(cancellationToken);
        return activities.Select(MapToDto).ToList();
    }

    public async Task<ActivityDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var activity = await activityRepository.GetByIdAsync(id, cancellationToken);
        return activity is null ? null : MapToDto(activity);
    }

    public async Task<ActivityDto> CreateAsync(ActivityCreateDto request, CancellationToken cancellationToken = default)
    {
        var name = request.Name.Trim();
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidOperationException("Activity name is required.");
        }

        var activity = ActivityFactory.Create(request.Name, request.Type, request.MaxParticipants, request.Price, request.AdditionalInfoField1, request.Level);

        await activityRepository.AddAsync(activity, cancellationToken);
        await activityRepository.SaveChangesAsync(cancellationToken);

        return MapToDto(activity);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var activity = await activityRepository.GetByIdAsync(id, cancellationToken);
        if (activity is null)
        {
            return false;
        }

        activityRepository.Delete(activity);
        await activityRepository.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static ActivityDto MapToDto(Activity activity) => new()
    {
        Id = activity.Id,
        Name = activity.Name,
        Type = activity.Type,
        MaxParticipants = activity.MaxParticipants,
        Price = activity.Price,
        AdditionalInfoField1 = activity.AdditionalInfoField1,
        Level = activity.Level
    };
}
