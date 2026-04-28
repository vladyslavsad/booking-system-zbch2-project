namespace bookingSystemZBC.Authorization.Interfaces
{
    public interface IOwnedResource
    {
        int OwnerId { get; }
    }
}
