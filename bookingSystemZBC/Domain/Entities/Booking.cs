using bookingSystemZBC.Authorization.Interfaces;

namespace bookingSystemZBC.Domain.Entities;

public class Booking : IOwnedResource
{
    public int Id { get; private set; }
    public int OwnerId => MemberId;
    public int MemberId { get; set; }
    public Member? Member { get; set; }
    public int ActivitySessionId { get; set; }
    public ActivitySession? ActivitySession { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}
