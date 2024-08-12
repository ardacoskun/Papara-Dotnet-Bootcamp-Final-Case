using Para.Schema.Entities.Models;

namespace Para.Data.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetByEmailAsync(string email);
}
