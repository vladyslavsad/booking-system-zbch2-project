namespace bookingSystemZBC.Domain.Entities
{
    public class Role
    {
        public int RoleId { get; private set; }
        public string RoleName { get; set; } = string.Empty;
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
