using System.ComponentModel.DataAnnotations;
using bookingSystemZBC.Domain.Enums;

namespace bookingSystemZBC.DTOs.Activities;

public class ActivityCreateDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public ActivityType Type { get; set; }

    [Range(1, 1000)]
    public int MaxParticipants { get; set; }

    [Range(typeof(double), "0", "999999")]
    public double Price { get; set; }

    public string? AdditionalInfoField1 { get; set; }
    public string? Level { get; set; }
}
