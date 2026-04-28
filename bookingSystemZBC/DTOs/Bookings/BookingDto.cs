namespace bookingSystemZBC.DTOs.Bookings;

public class BookingDto
{
    public int Id { get; set; }
    public int MemberId { get; set; }
    public string MemberName { get; set; } = string.Empty;
    public int ActivitySessionId { get; set; }
    public string ActivityName { get; set; } = string.Empty;
    public DateTime SessionStartTimeUtc { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}
