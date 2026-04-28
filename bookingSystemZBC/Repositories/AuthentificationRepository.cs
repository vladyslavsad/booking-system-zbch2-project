using bookingSystemZBC.Data;
using bookingSystemZBC.Domain.Entities;

namespace bookingSystemZBC.Repositories
{
    public class AuthentificationRepository (AppDbContext appDbContext) : Repository<User>(appDbContext)
    {

    }
}
