using System.ComponentModel.DataAnnotations;

namespace bookingSystemZBC.DTOs.Locations;

public class LocationCreateDto
{
    [Required]
    [MaxLength(120)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }

    [Range(0, int.MaxValue)]
    public int Capacity { get; set; }
}
