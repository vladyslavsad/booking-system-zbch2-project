using System.ComponentModel.DataAnnotations;

namespace bookingSystemZBC.DTOs.Members
{
    public class MemberCreateDTO
    {
        [Required]
        [MaxLength(120)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        [MaxLength(200)]
        public string Email { get; set; } = string.Empty;
        [Range(0, 150)]
        public int Age { get; set; }
    }
}
