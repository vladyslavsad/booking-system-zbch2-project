using bookingSystemZBC.Data;
using bookingSystemZBC.Domain.Entities;

namespace bookingSystemZBC.Repositories;

public class LocationRepository(AppDbContext dbContext) : Repository<Location>(dbContext), ILocationRepository
{
}
