using bookingSystemZBC.DTOs.Bookings;

namespace bookingSystemZBC.Services;

public interface IBookingServiceRCTest
{
    Task<IReadOnlyList<BookingDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<BookingDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<BookingDto> CreateAsync(BookingCreateDto request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
