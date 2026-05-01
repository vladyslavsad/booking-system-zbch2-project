using System.ComponentModel.DataAnnotations;

namespace bookingSystemZBC.Domain.Entities
{
    public class User
    {
        public int Id { get; private set; }

        [Required]
        [MaxLength(120)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(200)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [MaxLength(120)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(120)]
        public string Surname { get; set; } = string.Empty;

        public int? MemberId { get; set; } = null;
        public Member? Member { get; set; }
        public ICollection<Role> Roles { get; set; } = new List<Role>();
    }
}
