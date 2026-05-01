using bookingSystemZBC.Controllers;
using bookingSystemZBC.Domain.Entities.Activities;
using bookingSystemZBC.Repositories;

namespace bookingSystemZBC.Services
{
    public class PolymorfismService(IPolymorfismRepository polymorfismRepository) : IPolymorfismService
    {
        public async Task<IReadOnlyList<Activity>> GetAllActivities()
        {
            var activities = await polymorfismRepository.GetAllAsync();
            return activities;
        }
    }
}
