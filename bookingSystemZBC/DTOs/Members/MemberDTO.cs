using System.ComponentModel.DataAnnotations;

namespace bookingSystemZBC.DTOs.Members
{
    public class MemberDTO
    {
        public int Id { get; set; }     
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}
