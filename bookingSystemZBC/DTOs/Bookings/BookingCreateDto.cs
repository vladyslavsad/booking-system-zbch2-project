using System.ComponentModel.DataAnnotations;

namespace bookingSystemZBC.DTOs.Bookings;

public class BookingCreateDto
{
    [Range(1, int.MaxValue)]
    public int MemberId { get; set; }

    [Range(1, int.MaxValue)]
    public int ActivitySessionId { get; set; }
}
