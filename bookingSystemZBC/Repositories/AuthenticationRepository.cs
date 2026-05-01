using bookingSystemZBC.Data;
using bookingSystemZBC.Domain.Entities;

namespace bookingSystemZBC.Repositories
{
    public class AuthenticationRepository (AppDbContext appDbContext) : Repository<User>(appDbContext)
    {

    }
}
