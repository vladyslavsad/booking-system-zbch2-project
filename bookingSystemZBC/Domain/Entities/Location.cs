using System.ComponentModel.DataAnnotations;

namespace bookingSystemZBC.Domain.Entities;

public class Location
{
    public int Id { get; private set; }

    [Required]
    [MaxLength(120)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }

    public int Capacity { get; set; }

    public ICollection<ActivitySession> Sessions { get; set; } = new List<ActivitySession>();
}
