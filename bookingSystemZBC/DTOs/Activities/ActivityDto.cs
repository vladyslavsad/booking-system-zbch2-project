using bookingSystemZBC.Domain.Enums;

namespace bookingSystemZBC.DTOs.Activities;

public class ActivityDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ActivityType Type { get; set; }
    public int MaxParticipants { get; set; }
    public double Price { get; set; }
    public string? AdditionalInfoField1 { get; set; }
    public string? Level { get; set; }
}
