using bookingSystemZBC.Domain.Entities.Activities;

namespace bookingSystemZBC.Services
{
    public interface IPolymorfismService
    {
        public Task<IReadOnlyList<Activity>> GetAllActivities();
    }
}
