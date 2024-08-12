using Microsoft.EntityFrameworkCore;
using Para.Data.Context;
using Para.Data.Repositories.Interfaces;
using Para.Schema.Entities.Models;

namespace Para.Data.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) { }

    public async Task<User> GetByEmailAsync(string email) => await _context.Set<User>().FirstOrDefaultAsync(u => u.Email == email);
}
