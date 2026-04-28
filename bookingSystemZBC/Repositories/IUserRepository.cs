using bookingSystemZBC.Domain.Entities;

namespace bookingSystemZBC.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default);
}
