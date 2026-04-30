using bookingSystemZBC.Domain.Entities;
using bookingSystemZBC.DTOs.Bookings;
using bookingSystemZBC.Repositories;

namespace bookingSystemZBC.Services;

// Race condition test version of BookingService, without transaction handling or concurrency checks
// This is only for testing purposes to demonstrate the need for proper concurrency handling in the real BookingService implementation
public class BookingServiceRCTest(
    IBookingRepository bookingRepository,
    IActivitySessionRepository activitySessionRepository,
    IRepository<Member> memberRepository) : IBookingServiceRCTest
{
    public async Task<IReadOnlyList<BookingDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var bookings = await bookingRepository.GetAllDetailedAsync(cancellationToken);
        return bookings.Select(MapToDto).ToList();
    }

    public async Task<BookingDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var booking = await bookingRepository.GetByIdDetailedAsync(id, cancellationToken);
        return booking is null ? null : MapToDto(booking);
    }

    public async Task<BookingDto> CreateAsync(BookingCreateDto request, CancellationToken cancellationToken = default)
    {
        var member = await memberRepository.GetByIdAsync(request.MemberId, cancellationToken)
            ?? throw new KeyNotFoundException($"Member {request.MemberId} was not found.");

        var session = await activitySessionRepository.GetByIdDetailedAsync(request.ActivitySessionId, cancellationToken)
            ?? throw new KeyNotFoundException($"Activity session {request.ActivitySessionId} was not found.");

        var alreadyBooked = await bookingRepository.ExistsForMemberAndSessionAsync(
            request.MemberId,
            request.ActivitySessionId,
            cancellationToken);

        if (alreadyBooked)
        {
            throw new InvalidOperationException("Member already has a booking for this session.");
        }

        var capacityActivity = session.Activity?.MaxParticipants ?? 0;
        if (session.Bookings.Count >= capacityActivity)
        {
            throw new InvalidOperationException("Session capacity has been reached.");
        }

        var capacityLocation = session.Location?.Capacity ?? 0;
        if (session.Bookings.Count >= capacityLocation)
        {
            throw new InvalidOperationException("Location capacity has been reached.");
        }

            var booking = new Booking
        {
            MemberId = request.MemberId,
            ActivitySessionId = request.ActivitySessionId,
            CreatedAtUtc = DateTime.UtcNow
        };

        await bookingRepository.AddAsync(booking, cancellationToken);
        await bookingRepository.SaveChangesAsync(cancellationToken);

        booking.Member = member;
        booking.ActivitySession = session;

        return MapToDto(booking);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var booking = await bookingRepository.GetByIdAsync(id, cancellationToken);
        if (booking is null)
        {
            return false;
        }

        bookingRepository.Delete(booking);
        await bookingRepository.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static BookingDto MapToDto(Booking booking) => new()
    {
        Id = booking.Id,
        MemberId = booking.MemberId,
        MemberName = booking.Member?.Name ?? string.Empty,
        ActivitySessionId = booking.ActivitySessionId,
        ActivityName = booking.ActivitySession?.Activity?.Name ?? string.Empty,
        SessionStartTimeUtc = booking.ActivitySession?.StartTimeUtc ?? default,
        CreatedAtUtc = booking.CreatedAtUtc
    };
}
