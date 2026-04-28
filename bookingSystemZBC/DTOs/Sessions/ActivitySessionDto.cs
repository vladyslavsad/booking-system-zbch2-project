namespace bookingSystemZBC.DTOs.Sessions;

public class ActivitySessionDto
{
    public int Id { get; set; }
    public int ActivityId { get; set; }
    public string ActivityName { get; set; } = string.Empty;
    public int LocationId { get; set; }
    public string LocationName { get; set; } = string.Empty;
    public DateTime StartTimeUtc { get; set; }
    public DateTime EndTimeUtc { get; set; }
    public string? Notes { get; set; }
    public int BookingCount { get; set; }
    public int Capacity { get; set; }
}
