using bookingSystemZBC.Data;
using bookingSystemZBC.Domain.Entities.Activities;


namespace bookingSystemZBC.Repositories
{
    public class PolymorfismRepository(AppDbContext appDbContext) : Repository<Activity>(appDbContext), IPolymorfismRepository
    {
    }
}
