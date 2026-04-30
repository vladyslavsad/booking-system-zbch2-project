using bookingSystemZBC.Authorization.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace bookingSystemZBC.DTOs.Members
{
    public class MemberDTO : IOwnedResource
    {
        public int Id { get; set; }
        public int OwnerId => Id;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}
