using bookingSystemZBC.Domain.Entities;
using bookingSystemZBC.DTOs.Members;

namespace bookingSystemZBC.DTOs.Users
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public int? MemberId { get; set; }
        public MemberDTO? Member { get; set; } = null;
        public List<Role> Roles { get; set; } = new List<Role>();
    }
}
