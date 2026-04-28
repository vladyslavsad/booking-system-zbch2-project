using System.ComponentModel.DataAnnotations;

namespace bookingSystemZBC.DTOs.Sessions;

public class ActivitySessionCreateDto
{
    [Range(0, int.MaxValue)]
    public int ActivityId { get; set; }

    [Range(0, int.MaxValue)]
    public int LocationId { get; set; }

    public DateTime StartTimeUtc { get; set; }
    public DateTime EndTimeUtc { get; set; }

    [MaxLength(400)]
    public string? Notes { get; set; }
}
