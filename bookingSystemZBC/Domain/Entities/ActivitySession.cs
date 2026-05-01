using bookingSystemZBC.Domain.Entities.Activities;
using System.ComponentModel.DataAnnotations;

namespace bookingSystemZBC.Domain.Entities;

public class ActivitySession
{
    public int Id { get; private set; }
    public int ActivityId { get; set; }
    public Activity? Activity { get; set; }
    public int LocationId { get; set; }
    public Location? Location { get; set; }
    public DateTime StartTimeUtc { get; set; }
    public DateTime EndTimeUtc { get; set; }

    [MaxLength(400)]
    public string? Notes { get; set; }

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
