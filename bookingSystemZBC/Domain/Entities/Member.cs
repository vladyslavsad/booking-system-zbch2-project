using bookingSystemZBC.Authorization.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace bookingSystemZBC.Domain.Entities;

public class Member : IOwnedResource
{
    public int Id { get; set; }
    public int OwnerId => Id;

    [Required]
    [MaxLength(120)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(200)]
    public string Email { get; set; } = string.Empty;

    [Range(0, 150)]
    public int Age { get; set; }

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    //public User? User { get; set; }
}
