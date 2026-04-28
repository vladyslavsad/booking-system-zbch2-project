using System.ComponentModel.DataAnnotations;

namespace bookingSystemZBC.DTOs.Users
{
    public class UserCreateDTO
    {
        [Required]
        [MaxLength(120)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(200)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        [MaxLength(500)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [MaxLength(120)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(120)]
        public string Surname { get; set; } = string.Empty;

        [Required]
        [Range(1, 150)]
        public int Age { get; set; }

        public int? MemberId { get; set; }
    }
}
